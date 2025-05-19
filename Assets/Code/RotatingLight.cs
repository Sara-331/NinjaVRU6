using UnityEngine;
using UnityEngine.SceneManagement;

public class RotatingLight : MonoBehaviour
{
     public float moveDistance = 2f;
    public float moveSpeed = 2f;
    public AudioSource loseSound;

    private Vector3 startPos;
    private bool movingRight = true;
    private bool hasLost = false;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (hasLost) return;

        float moveStep = moveSpeed * Time.deltaTime;
        if (movingRight)
        {
            transform.Translate(Vector3.right * moveStep);
            if (Vector3.Distance(transform.position, startPos) >= moveDistance)
                movingRight = false;
        }
        else
        {
            transform.Translate(Vector3.left * moveStep);
            if (Vector3.Distance(transform.position, startPos) >= moveDistance)
                movingRight = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasLost) return;

        if (other.CompareTag("Player"))
        {
            hasLost = true;
            Invoke("Lose", 3f); 
        }
    }

    void Lose()
    {
        if (loseSound != null)
            loseSound.Play();

        Invoke("GoToLobby", 1f);
    }

    void GoToLobby()
    {
        SceneManager.LoadScene(0);
    }
}