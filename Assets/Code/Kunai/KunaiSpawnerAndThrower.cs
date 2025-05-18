using UnityEngine;

public class KunaiSpawnerAndThrower : MonoBehaviour
{
    public OVRHand rightHand;               // Assign right hand OVRHand here in inspector
    public Transform rightHandPalm;         // Assign palm or wrist transform for spawn position

    public GameObject teleportKunaiPrefab;  // Assign teleport kunai prefab here
    public GameObject decoyKunaiPrefab;     // Assign decoy kunai prefab here
    public GameObject fireKunaiPrefab;      // Assign fire kunai prefab here

    private GameObject currentKunai = null;
    private Rigidbody kunaiRb = null;

    private bool wasHoldingKunaiLastFrame = false; // Track if holding any kunai

    private Vector3 lastPalmPos;
    private Vector3 handVelocity;

    private enum KunaiType { None, Teleport, Decoy, Fire }
    private KunaiType currentKunaiType = KunaiType.None;

    void Update()
    {
        if (rightHand == null)
        {
            Debug.LogWarning("Right hand not assigned!");
            return;
        }

        // Calculate hand velocity based on palm position delta
        handVelocity = (rightHandPalm.position - lastPalmPos) / Time.deltaTime;
        lastPalmPos = rightHandPalm.position;

        // Get pinch states
        bool indexPinch = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool middlePinch = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
        bool ringPinch = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
        bool thumbPinch = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Thumb);

        // Check gestures (relaxed conditions for better reliability)
        bool teleportGesture = indexPinch && !middlePinch;
        bool decoyGesture = thumbPinch && middlePinch && !ringPinch && !indexPinch;
        bool fireGesture = thumbPinch && ringPinch && !middlePinch && !indexPinch;

        bool isPinchingAny = indexPinch || middlePinch || ringPinch || thumbPinch;

        // Spawn kunai on gesture start
        if (!wasHoldingKunaiLastFrame)
        {
            if (teleportGesture)
            {
                Debug.Log("Spawning Teleport Kunai");
                SpawnKunai(teleportKunaiPrefab, KunaiType.Teleport);
            }
            else if (decoyGesture)
            {
                Debug.Log("Spawning Decoy Kunai");
                SpawnKunai(decoyKunaiPrefab, KunaiType.Decoy);
            }
            else if (fireGesture)
            {
                Debug.Log("Spawning Fire Kunai");
                SpawnKunai(fireKunaiPrefab, KunaiType.Fire);
            }
        }

        // If holding kunai and pinch still active, update position
        if (currentKunai != null && isPinchingAny)
        {
            currentKunai.transform.position = rightHandPalm.position;
            currentKunai.transform.rotation = rightHandPalm.rotation;
        }

        // If pinch released, throw kunai
        if (wasHoldingKunaiLastFrame && !isPinchingAny)
        {
            ThrowKunai();
        }

        wasHoldingKunaiLastFrame = isPinchingAny;
    }

    void SpawnKunai(GameObject prefab, KunaiType type)
    {
        if (currentKunai != null) return; // Only one kunai at a time

        if (prefab == null)
        {
            Debug.LogError($"Prefab for {type} kunai is not assigned!");
            return;
        }

        currentKunai = Instantiate(prefab, rightHandPalm.position, rightHandPalm.rotation);
        kunaiRb = currentKunai.GetComponent<Rigidbody>();

        if (kunaiRb != null)
        {
            kunaiRb.isKinematic = true; // Disable physics while holding
        }

        currentKunai.transform.SetParent(rightHandPalm);

        currentKunaiType = type;
    }

    void ThrowKunai()
    {
        if (currentKunai == null) return;

        // Detach from hand
        currentKunai.transform.SetParent(null);

        if (kunaiRb != null)
        {
            kunaiRb.isKinematic = false;

            // Apply hand velocity for throw
            kunaiRb.linearVelocity = handVelocity;
            kunaiRb.angularVelocity = Vector3.zero;
        }

        currentKunai = null;
        kunaiRb = null;
        currentKunaiType = KunaiType.None;
    }
}
