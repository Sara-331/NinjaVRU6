using UnityEngine;
using UnityEngine.SceneManagement;

public class GateTrigger : MonoBehaviour
{
    public string targetTag = "Player";
    public int targetSceneIndex; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
           
            if (ProgressManager.instance != null && ProgressManager.instance.currentStage >= targetSceneIndex - 1)
            {
                SceneManager.LoadScene(targetSceneIndex);
            }
            else
            {
                Debug.Log("Not Avlible");
            }
        }
    }
}
