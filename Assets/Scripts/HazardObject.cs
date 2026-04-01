using UnityEngine;

public class HazardObject : MonoBehaviour
{
    public float damageAmount = 10f;

    // Called when something enters collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Get PlayerHealth and PlayerMovement
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            PlayerMovement movement = other.GetComponent<PlayerMovement>();

            Debug.Log("Health comp found: " + (health != null));

            // Deal damage if the component exists
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }

            // Cancel the speed boost if one is running
            if (movement != null)
            {
                movement.CancelBoost();
            }
        }
    }
}
