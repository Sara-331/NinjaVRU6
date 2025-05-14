using UnityEngine;

public class ReturnToLobby : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ProgressManager.instance.ReturnFromStage();
        }
    }
}
