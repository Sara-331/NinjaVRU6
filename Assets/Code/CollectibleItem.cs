using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public DoorTeleportOnCollect doorManager;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!collected && other.CompareTag("Player"))
        {
            collected = true;
            doorManager.CollectItem();
            Destroy(gameObject);
        }
    }
}
