using UnityEngine;
using UnityEngine.SceneManagement;

public class GateTrigger : MonoBehaviour
{
    public string targetTag = "Player"; 
    public int targetSceneIndex ; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            SceneManager.LoadScene(targetSceneIndex);
        }
    }
}
