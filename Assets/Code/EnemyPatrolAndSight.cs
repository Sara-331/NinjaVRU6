using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyPatrolAndSight : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 2f;
    public Transform player;
    public float detectionRange = 2f;

    public GameObject alertUI;      
    public Slider alertSlider;      

    private float detectionTimer = 0f;
    public float timeToCatch = 5f;

    public AudioSource catchSound;

    private int currentPoint = 0;
    private bool playerInRange = false;

    void Start()
    {
        if (alertUI != null)
            alertUI.SetActive(false);
    }

    void Update()
    {
        Patrol();
        DetectPlayer();
    }

    void DetectPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        playerInRange = distance <= detectionRange;

        if (playerInRange)
        {
            if (!alertUI.activeSelf)
                alertUI.SetActive(true); 
            detectionTimer += Time.deltaTime;
            alertSlider.value = detectionTimer / timeToCatch;

            if (detectionTimer >= timeToCatch)
            {
                if (catchSound != null)
                    catchSound.Play();

                SceneManager.LoadScene("Lobby");
            }
        }
        else
        {
            detectionTimer = Mathf.Max(0, detectionTimer - Time.deltaTime); // يقل إذا ابتعد اللاعب
            alertSlider.value = detectionTimer / timeToCatch;

            if (detectionTimer <= 0.01f && alertUI.activeSelf)
                alertUI.SetActive(false); 
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0 || playerInRange) return;

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
}
