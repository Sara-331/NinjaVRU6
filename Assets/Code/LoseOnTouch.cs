using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseOnTouch : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("Light"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
