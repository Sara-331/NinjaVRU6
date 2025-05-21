using UnityEngine;
using System.Collections;

public class DecoyKunai : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject decoyCharacterPrefab;   // The decoy to spawn
    public GameObject player;                 // The player GameObject
    public float decoyDuration = 5f;          // Duration decoy lasts

    private bool hasTriggered = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasTriggered) return;
        hasTriggered = true;

        Debug.Log("Decoy Kunai hit: " + collision.gameObject.name);

        // Spawn decoy at impact location
        if (decoyCharacterPrefab != null)
        {
            Instantiate(decoyCharacterPrefab, transform.position, Quaternion.identity);
        }

        // Start decoy effect coroutine
        if (player != null)
        {
            StartCoroutine(DecoyEffect());
        }

        // Destroy the kunai after a short delay (to avoid instant pop)
        Destroy(gameObject, 0.2f);
    }

    private IEnumerator DecoyEffect()
    {
        // Temporarily hide the player
        SetPlayerVisibility(false);
        player.tag = "Untagged";

        yield return new WaitForSeconds(decoyDuration);

        // Re-enable player visibility and tag
        SetPlayerVisibility(true);
        player.tag = "Player";
    }

    private void SetPlayerVisibility(bool visible)
    {
        Renderer[] renderers = player.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.enabled = visible;
        }

        // Optional: disable colliders if needed
        Collider[] colliders = player.GetComponentsInChildren<Collider>();
        foreach (Collider c in colliders)
        {
            c.enabled = visible;
        }
    }
}
