using UnityEngine;

public class SimpleSocket : MonoBehaviour
{
    public Transform attachPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kunai"))
        {
            other.transform.position = attachPoint.position;
            other.transform.rotation = attachPoint.rotation;

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;
        }
    }
}
