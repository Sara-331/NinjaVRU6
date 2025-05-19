using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public AudioSource collectSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (collectSound != null)
                collectSound.Play();

            GateSystem gateSystem = FindObjectOfType<GateSystem>();
            if (gateSystem != null)
                gateSystem.CollectItem();

            Destroy(gameObject);
        }
    }
}
