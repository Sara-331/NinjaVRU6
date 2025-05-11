using UnityEngine;

public class ReturnToLobby : MonoBehaviour
{
    public ProgressManager progressManager; // هذا الكود احطه جوا المرحلة كنقطة عودة للوبي في حال لمسه اللاعب 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            progressManager.ReturnFromStage();
        }
    }
}
