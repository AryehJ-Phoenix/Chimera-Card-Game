using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerControl : MonoBehaviour
{
    Vector2 moveInput;
    Vector2 boundaries = new(34,15);
    [SerializeField] float target_speed = 5;
    [SerializeField] float acceleration = 25f;
    new Rigidbody2D rigidbody;
    float last_directionX = 1;
    public bool canMove = true;
    public float timeUntilMove = -2;
    int collisions = 0;
    float health = 10;
    public Vector2 oldSpeed;
    public bool damaging = false;
    GameManager GM;
    //float last_directionY = 1;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        GM = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float vX = moveInput.x * target_speed;
        float vY = moveInput.y * target_speed;
        if (canMove)
        {
            rigidbody.linearVelocity = new Vector2(Mathf.MoveTowards(rigidbody.linearVelocityX,vX,acceleration*Time.deltaTime),Mathf.MoveTowards(rigidbody.linearVelocityY,vY,acceleration*Time.deltaTime));
        }
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

        timeUntilMove -= Time.deltaTime;
        if (timeUntilMove <= 0 && collisions == 0) {canMove = true; GetComponent<BoxCollider2D>().isTrigger = false; damaging = false;}
        if (timeUntilMove <= 0 && timeUntilMove > -1 && collisions == 0) {rigidbody.linearVelocity /= 2; rigidbody.linearVelocity += oldSpeed/4; timeUntilMove = -2;}
        if (timeUntilMove > 0) {canMove = false; GetComponent<BoxCollider2D>().isTrigger = true; damaging = true;}
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // moveInput = context.ReadValue<float>();
        moveInput = context.ReadValue<Vector2>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        collisions++;
        print(collision.gameObject);
        if (collision.CompareTag("Enemy"))
        {
            print("HIT " + collision.gameObject.name);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        collisions--;
    }

    public void ChangeHealth(float amount)
    {
        float old = health;
        health += amount;
        print("Player Health Changed from " + old + " to " + health);
    }
}