using UnityEngine;

public class Goblin : MonoBehaviour
{
    GameManager GM;
    float speed = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GM = FindAnyObjectByType<GameManager>();
        print(GM);
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.Player != null)
        {
            print("MOVING");
            Vector2.MoveTowards(transform.position,GM.Player.transform.position,speed*Time.deltaTime);
        }
    }
}
