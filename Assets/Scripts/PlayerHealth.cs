using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float maxHP = 100f;
    private float currentHP;
    public float healAmount = 25f;
    private PlayerMovement movement;
    public HUDController hud;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = maxHP;
        movement = GetComponent<PlayerMovement>();
    }

    public void Heal()
    {
        // Heal the player and ensure HP does not exceed maxHP
        currentHP = Mathf.Clamp(currentHP + healAmount, 0, maxHP);

        Debug.Log("Player healed. Current HP: " + currentHP);

        if (hud != null)
        {
            hud.UpdateHP(currentHP, maxHP);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHP = Mathf.Clamp(currentHP - amount, 0, maxHP);
        Debug.Log("Player took damage. Current HP: " + currentHP);

        if (hud != null)
        {
            hud.UpdateHP(currentHP, maxHP);
        }

        // Trigger hurt animation and stun
        if (movement != null)
        {
            movement.TriggerHurt();
        }

        if (currentHP <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
