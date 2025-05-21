using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float visionDistance = 10f;
    public float detectionRange = 1f;
    public float timeToCatch = 5f;

    public GameObject blackoutUI;
    public AudioSource catchSound;
    public Image fillImage;

    private bool isDistracted = false;
    private float distractTimer = 0f;
    private Vector3 decoyPosition;

    private float detectionTimer = 0f;
    private bool isLosing = false;

    void Start()
    {
        if (blackoutUI != null)
        {
            blackoutUI.SetActive(false);
            blackoutUI.transform.SetParent(Camera.main.transform);
            blackoutUI.transform.localPosition = new Vector3(0, 0, 2);
            blackoutUI.transform.localRotation = Quaternion.identity;
        }

        if (fillImage != null)
            fillImage.fillAmount = 0f;
    }

    void Update()
    {
        if (isLosing || player == null) return;

        if (isDistracted)
        {
            distractTimer -= Time.deltaTime;
            if (distractTimer <= 0)
            {
                isDistracted = false;
            }
            else
            {
                agent.SetDestination(decoyPosition);
                return;
            }
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            // Catch logic
            agent.isStopped = true;
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
            // Chase player
            agent.isStopped = false;
            agent.SetDestination(player.position);
            detectionTimer = Mathf.Max(0f, detectionTimer - Time.deltaTime);
            if (fillImage != null)
                fillImage.fillAmount = detectionTimer / timeToCatch;
        }
    }

    public void DistractWithDecoy(Vector3 position, float duration)
    {
        isDistracted = true;
        distractTimer = duration;
        decoyPosition = position;
        agent.SetDestination(position);
        Debug.Log($"{gameObject.name} is distracted by decoy.");
    }

    void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void LoadLobbyScene()
    {
        SceneManager.LoadScene(0); // Or use a named scene like SceneManager.LoadScene("Lobby");
    }
}
