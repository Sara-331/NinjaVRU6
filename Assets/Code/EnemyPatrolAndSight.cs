using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float visionDistance = 10f;
    public float detectionRange = 5f;
    public float timeToCatch = 5f;

    public float roamRadius = 10f;
    public float roamInterval = 4f;

    public GameObject blackoutUI;
    public AudioSource catchSound;
    public Image fillImage;

    private bool isDistracted = false;
    private float distractTimer = 0f;
    private Vector3 decoyPosition;

    private float detectionTimer = 0f;
    private bool isLosing = false;

    private Vector3 lastDestination;
    private float destinationThreshold = 0.5f;

    private float roamTimer = 0f;
    private float defaultSpeed = 3.5f;
    private float defaultAcceleration = 8f;

    void Start()
    {
        if (agent != null)
        {
            agent.speed = defaultSpeed;
            agent.acceleration = defaultAcceleration;
        }

        if (blackoutUI != null)
        {
            blackoutUI.SetActive(false);
            blackoutUI.transform.SetParent(Camera.main.transform);
            blackoutUI.transform.localPosition = new Vector3(0, 0, 2);
            blackoutUI.transform.localRotation = Quaternion.identity;
        }

        if (fillImage != null)
            fillImage.fillAmount = 0f;

        lastDestination = transform.position;
    }

    void Update()
    {
        if (isLosing || player == null || agent == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (isDistracted)
        {
            distractTimer -= Time.deltaTime;
            if (distractTimer <= 0)
            {
                isDistracted = false;
            }
            else
            {
                UpdateDestination(decoyPosition);
                return;
            }
        }

        if (distance <= detectionRange)
        {
            agent.isStopped = true;

            if (agent.velocity.magnitude < 0.1f)
                FaceTarget(player);

            detectionTimer += Time.deltaTime;
            if (fillImage != null)
                fillImage.fillAmount = detectionTimer / timeToCatch;

            if (detectionTimer >= timeToCatch && !isLosing)
            {
                isLosing = true;
                if (catchSound != null) catchSound.Play();
                if (blackoutUI != null) blackoutUI.SetActive(true);
                Invoke("LoadLobbyScene", 1.5f);
            }
        }
        else if (distance <= visionDistance)
        {
            agent.isStopped = false;

            detectionTimer = Mathf.Max(0f, detectionTimer - Time.deltaTime);
            if (fillImage != null)
                fillImage.fillAmount = detectionTimer / timeToCatch;

            UpdateDestination(player.position);
        }
        else
        {
            RoamRandomly();
        }
    }

    void RoamRandomly()
    {
        roamTimer -= Time.deltaTime;

        if (roamTimer <= 0f)
        {
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            randomDirection += transform.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, roamRadius, NavMesh.AllAreas))
            {
                UpdateDestination(hit.position);
            }

            roamTimer = roamInterval;
        }
    }

    void UpdateDestination(Vector3 targetPosition)
    {
        if (Vector3.Distance(lastDestination, targetPosition) > destinationThreshold)
        {
            agent.SetDestination(targetPosition);
            lastDestination = targetPosition;
        }
    }

    public void DistractWithDecoy(Vector3 position, float duration)
    {
        isDistracted = true;
        distractTimer = duration;
        decoyPosition = position;
        agent.isStopped = false;
        UpdateDestination(decoyPosition);
        Debug.Log($"{gameObject.name} is distracted by decoy.");
    }

    void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0f;

        if (direction.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
        }
    }

    void LoadLobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }
}
