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
    private Animator animator;
    private bool isStunned = false;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Store value of original speed before boost
        baseSpeed = moveSpeed;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Called when the move action is triggered
    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            // Always store the input, even during stun
            moveInput = context.ReadValue<UnityEngine.Vector2>().x;

            if (!isStunned)
            {
                animator.SetBool("isRunning", true);
            }
        }

        if (context.canceled)
        {
            moveInput = 0f;
            animator.SetBool("isRunning", false);
        }
    }

    void FixedUpdate()
    {
        // Don't apply movement if stunned
        if (isStunned)
        {
            rb.linearVelocity = new UnityEngine.Vector2(0f, rb.linearVelocity.y);
            return;
        }

        rb.linearVelocity = new UnityEngine.Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Flip sprite based on direction
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    // Called when the jump action is triggered
    public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started && !isStunned)
        {
            rb.linearVelocity = new UnityEngine.Vector2(rb.linearVelocity.x, jumpForce);

            // Trigger jump animation
            animator.SetTrigger("Jump");
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

    // Called when damage is taken
    public void TriggerHurt()
    {
        animator.SetTrigger("Hurt");
        animator.SetBool("isHurt", true);
        StartCoroutine(StunRoutine());
        Debug.Log("IsStunned: " + isStunned);
    }

    // Stops player from moving durinf hurt animation
    private IEnumerator StunRoutine()
    {
        isStunned = true;
        moveInput = 0f;
        animator.SetBool("isRunning", false);

        //Animation finish
        yield return new WaitForSeconds(0.5f);

        isStunned = false;

        animator.SetBool("isHurt", false);

        if (moveInput != 0f)
        {
            animator.SetBool("isRunning", true);
        }
    }

    public void EndStun()
    {
        isStunned = false;
        Debug.Log("Stun ended");

        // If player already holding direction, resume run
        if (moveInput != 0f)
        {
            animator.SetBool("isRunning", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
