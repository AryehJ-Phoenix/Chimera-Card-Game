using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public Vector2 dir;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)dir*speed*Time.deltaTime;
    }
}
