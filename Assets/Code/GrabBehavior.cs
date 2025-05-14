using UnityEngine;

public class SimpleGrab : MonoBehaviour
{
    private OVRGrabber grabber;

    void Start()
    {
        grabber = GetComponent<OVRGrabber>();
    }

    void OnTriggerEnter(Collider other)
    {
    }
}
