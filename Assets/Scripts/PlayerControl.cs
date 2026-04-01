using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] float target_speed = 5;
    [SerializeField] float acceleration = 25f;
    Rigidbody2D rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float vX = moveInput.x * target_speed;
        float vY = moveInput.y * target_speed;
        rigidbody.linearVelocity = new Vector2(Mathf.MoveTowards(rigidbody.linearVelocityX,vX,acceleration*Time.deltaTime),Mathf.MoveTowards(rigidbody.linearVelocityY,vY,acceleration*Time.deltaTime));
        //transform.Translate(moveInput*speed,Space.World);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        print("moving");
        // moveInput = context.ReadValue<float>();
        moveInput = context.ReadValue<Vector2>();
        print(moveInput);
    }
}
