using UnityEngine;

public class TeleportKunai : MonoBehaviour
{
    private bool hasTeleported = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasTeleported) return;

        // Find OVRCameraRig root
        GameObject rigRoot = GameObject.Find("OVRCameraRig")?.transform.root.gameObject;

        if (rigRoot == null)
        {
            Debug.LogWarning("OVRCameraRig not found!");
            return;
        }

        // Add upward offset to avoid getting stuck in floor
        Vector3 teleportPosition = transform.position + Vector3.up * 1.5f;

        // Move the entire rig
        rigRoot.transform.position = teleportPosition;

        Debug.Log("Teleported to kunai position.");
        hasTeleported = true;

        Destroy(gameObject, 0.2f); // Optional: destroy kunai after teleport
    }
}
