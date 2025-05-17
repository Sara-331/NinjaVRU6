using UnityEngine;
using System.Collections;

public class DecoyKunai : MonoBehaviour
{
    public GameObject decoyCharacterPrefab; // Assign decoy character prefab in inspector
    public float decoyDuration = 5f;         // How long the decoy lasts
    public GameObject player;                // Assign the player GameObject here

    private bool hasTriggered = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasTriggered) return;

        // Only trigger on ground or suitable surfaces
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            hasTriggered = true;

            // Spawn decoy character at kunai position
            Instantiate(decoyCharacterPrefab, transform.position, Quaternion.identity);

            // Start decoy effect coroutine
            StartCoroutine(DecoyEffect());

            // Destroy kunai object after spawning decoy
            Destroy(gameObject);
        }
    }

    private IEnumerator DecoyEffect()
    {
        if (player != null)
        {
            // Disable player tag (to hide or stop interactions)
            player.tag = "Untagged";

            // Optionally disable player renderer or collider here for invisibility
            SetPlayerVisibility(false);

            // Wait for duration
            yield return new WaitForSeconds(decoyDuration);

            // Re-enable player tag
            player.tag = "Player";

            SetPlayerVisibility(true);
        }
    }

    private void SetPlayerVisibility(bool visible)
    {
        // Example: disable all renderers on player for invisibility
        Renderer[] renderers = player.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.enabled = visible;
        }

        // You can also disable colliders or other components here if needed
    }
}
