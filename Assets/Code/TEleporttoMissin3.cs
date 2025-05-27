using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportOnTrigger : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene to load

    private void OnTriggerEnter(Collider other)
    {
        // Make sure only the player triggers the teleport
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
