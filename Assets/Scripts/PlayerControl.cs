using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public Slots s1;
    public Slots s2;
    public Slots s3;
    Vector2 moveInput;
    Vector2 boundaries = new Vector2(34,15);
    [SerializeField] float target_speed = 5;
    [SerializeField] float acceleration = 25f;
    new Rigidbody2D rigidbody;
    float last_directionX = 1; //DO THIS FOR SPRITE FLIPPING
    float last_directionY = 1;
    

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

        if(transform.position.x > boundaries.x) {rigidbody.linearVelocityX = -20;}
        if(transform.position.x < -boundaries.x) {rigidbody.linearVelocityX = 20;}
        if(transform.position.y > boundaries.y) {rigidbody.linearVelocityY = -20;}
        if(transform.position.y < -boundaries.y) {rigidbody.linearVelocityY = 20;}

        if(Mathf.Abs(rigidbody.linearVelocityX) < 10 && Mathf.Abs(rigidbody.linearVelocityY) < 10) {GetComponent<Animator>().speed = 0.5f;}
        else {GetComponent<Animator>().speed = 1;}

        if(last_directionX == 1) {GetComponent<SpriteRenderer>().flipX = false;}
        else {GetComponent<SpriteRenderer>().flipX = true;}

        if (rigidbody.linearVelocityX > 0) {last_directionX = 1;}
        if (rigidbody.linearVelocityX < 0) {last_directionX = -1;}
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // moveInput = context.ReadValue<float>();
        moveInput = context.ReadValue<Vector2>();
    }
}