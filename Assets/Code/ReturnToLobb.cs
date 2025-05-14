using UnityEngine;

public class ReturnToLobby : MonoBehaviour
{
    public ProgressManager progressManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            progressManager.ReturnFromStage();
        }
    }
}
