using UnityEngine;

public class ReturnToLobby : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player") && GateSystem.instance.HasCollectedAll())
    {
        Debug.Log("Player touched the gate and has collected all.");
        ProgressManager.instance.ReturnFromStage();
    }
    }
}
