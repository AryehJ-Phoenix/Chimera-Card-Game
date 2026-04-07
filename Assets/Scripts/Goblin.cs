using UnityEditor.U2D;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    GameManager GM;
    float speed = 5f;
    public float speedMultiplier = 1;
    SpriteRenderer sprite;
    [SerializeField] Sprite normSprite;
    [SerializeField] Sprite punchSprite;
    [SerializeField] Sprite throwSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = normSprite;
        GM = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.Player != null)
        {
            if (Vector2.Distance(transform.position,GM.Player.transform.position) > 2.5)
            {
                transform.position = Vector2.MoveTowards(transform.position,GM.Player.transform.position,speedMultiplier*speed*Time.deltaTime);
            }
            else
            {
                
            }
        }
    }
}
