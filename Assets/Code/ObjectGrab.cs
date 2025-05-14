using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;  
    }

    void OnGrab()
    {
        rb.isKinematic = false; 
    }

    void OnRelease()
    {
        rb.isKinematic = true;   
    }
}
