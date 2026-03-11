using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public float speed = 5;
    public Vector2 movement;
    public AudioSource SFX;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Use with a stick
        //transform.position += (Vector3)movement * speed * Time.deltaTime;

        //Use mouse position
        transform.position = movement;
    
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack" + context.phase);

        if(context.performed)
        {
            GetComponent<Interact>
            SFX.Play();
        }
    }

    public void OnPoint(InputAction.CallbackContext context)
    {

        //The same as Mouse.current.position.ReadValue()
        movement = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }
}
