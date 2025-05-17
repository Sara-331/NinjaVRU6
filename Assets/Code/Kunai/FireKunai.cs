using UnityEngine;

public class FireKunai : MonoBehaviour
{
    public float burnDuration = 3f;  // How long the burn lasts
    public ParticleSystem fireEffect; // Optional fire particle effect on collision

    private bool hasBurned = false;

    void OnCollisionEnter(Collision collision)
    {
        if (hasBurned) return; // Prevent multiple triggers

        // Check if the collided object is tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hasBurned = true;

            // Start burning effect and eventually destroy the hit object
            StartCoroutine(BurnCoroutine(collision.gameObject));

            // Optional: play fire particles
            if (fireEffect != null)
            {
                fireEffect.transform.parent = null;  // Detach so it can keep playing
                fireEffect.Play();
            }

            // Disable collider and physics to "stick" in place
            Collider col = GetComponent<Collider>();
            if (col != null) col.enabled = false;

            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;
        }
    }

    private System.Collections.IEnumerator BurnCoroutine(GameObject target)
    {
        Debug.Log($"FireKunai started burning {target.name}");

        // Wait burn duration
        yield return new WaitForSeconds(burnDuration);

        Debug.Log($"FireKunai finished burning {target.name}");

        // Destroy the target object
        if (target != null)
        {
            Destroy(target);
        }

        // Destroy fire effect and kunai object
        if (fireEffect != null)
        {
            Destroy(fireEffect.gameObject, 1f);
        }

        Destroy(gameObject);
    }
}
