using UnityEngine;
using UnityEngine.Events;

public class PickupItem : MonoBehaviour
{
    public UnityEvent OnPickup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if object is player
        if (other.CompareTag("Player"))
        {
            OnPickup.Invoke();

            Destroy(gameObject);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
