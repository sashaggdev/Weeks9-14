using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7;
    private Rigidbody2D rb;
    private float moveInput;
    public float boostMultiplier = 2f;
    public float boostDuration = 5f;
    private float baseSpeed;
    private Coroutine boostCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Store value of original speed before boost
        baseSpeed = moveSpeed;
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

    // Called by Pickup B (Speed boost)
    public void SpeedBoost()
    {
        // Check if a boost is already running, stop the running boost before starting new one
        if (boostCoroutine != null)
        {
            StopCoroutine(boostCoroutine);
        }

        boostCoroutine = StartCoroutine(SpeedBoostRoutine());
    }

    private IEnumerator SpeedBoostRoutine()
    {
        // Apply boost
        moveSpeed = baseSpeed * boostMultiplier;
        Debug.Log("Speed boost started.");

        // Wait for boostDuration
        yield return new WaitForSeconds(boostDuration);

        // Return to original speed
        moveSpeed = baseSpeed;
        Debug.Log("Speed boost ended.");
    }

    // Called when damage is taken, stops boost
    public void CancelBoost()
    {
        if (boostCoroutine != null)
        {
            StopCoroutine(boostCoroutine);
            boostCoroutine = null;
            moveSpeed = baseSpeed;
            Debug.Log("Speed boost cancelled");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
