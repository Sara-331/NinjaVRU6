using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleportOnCollect : MonoBehaviour
{
    public GameObject explosionEffectObject; // Use GameObject instead of ParticleSystem
    public string targetSceneName = "NextScene";

    private int itemsCollected = 0;
    private bool doorReady = false;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;

        if (explosionEffectObject != null)
            explosionEffectObject.SetActive(false); // Make sure it's off at the start
    }

    public void CollectItem()
    {
        itemsCollected++;

        if (itemsCollected >= 2 && !doorReady)
        {
            doorReady = true;

            if (explosionEffectObject != null)
                explosionEffectObject.SetActive(true); // Activate explosion
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (doorReady && other.CompareTag("Player"))
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
