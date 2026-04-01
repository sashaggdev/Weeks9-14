using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7;
    private Rigidbody2D rb;
    private float moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Called when the move action is triggered
    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
       if (context.performed)
        {
            moveInput = context.ReadValue<UnityEngine.Vector2>().x;
        }

        if (context.canceled)
        {
            moveInput = 0f;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new UnityEngine.Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    // Called when the jump action is triggered
    public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rb.linearVelocity = new UnityEngine.Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
