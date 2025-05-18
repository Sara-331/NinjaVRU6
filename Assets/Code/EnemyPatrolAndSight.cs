using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyPatrolAndSight : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 2f;
    public Transform player;
    public float detectionRange = 2f;
    public float timeToCatch = 5f;
    public GameObject blackoutUI;
    public AudioSource catchSound;
    public Image fillImage;

    public float decoyDetectionRange = 10f;

    private int currentPoint = 0;
    private float detectionTimer = 0f;
    private bool playerInRange = false;
    private bool isLosing = false;

    private GameObject currentDecoyTarget;

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
        FindNearestDecoy();
        PatrolAndDetect();
    }

    void FindNearestDecoy()
    {
        GameObject[] decoys = GameObject.FindGameObjectsWithTag("Decoy");
        float closestDistance = Mathf.Infinity;
        currentDecoyTarget = null;

        foreach (GameObject decoy in decoys)
        {
            float dist = Vector3.Distance(transform.position, decoy.transform.position);
            if (dist < decoyDetectionRange && dist < closestDistance)
            {
                closestDistance = dist;
                currentDecoyTarget = decoy;
            }
        }
    }

    void PatrolAndDetect()
    {
        Transform target = currentDecoyTarget != null ? currentDecoyTarget.transform : player;
        float distance = Vector3.Distance(transform.position, target.position);
        playerInRange = distance <= detectionRange;

        if (playerInRange && !isLosing)
        {
            Vector3 lookDirection = target.position - transform.position;
            lookDirection.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);

            detectionTimer += Time.deltaTime;

            if (fillImage != null)
                fillImage.fillAmount = detectionTimer / timeToCatch;

            if (detectionTimer >= timeToCatch)
            {
                isLosing = true;

                if (catchSound != null)
                    catchSound.Play();

                if (blackoutUI != null)
                    blackoutUI.SetActive(true);

                Invoke("LoadLobbyScene", 1.5f);
            }
        }
        else
        {
            detectionTimer = Mathf.Max(0f, detectionTimer - Time.deltaTime);

            if (fillImage != null)
                fillImage.fillAmount = detectionTimer / timeToCatch;

            if (!playerInRange && patrolPoints.Length > 0)
                Patrol();
        }
    }

    void Patrol()
    {
        Transform targetPoint = patrolPoints[currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        Vector3 dir = targetPoint.position - transform.position;
        if (dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }

    void LoadLobbyScene()
    {
        SceneManager.LoadScene(0);
    }
}
