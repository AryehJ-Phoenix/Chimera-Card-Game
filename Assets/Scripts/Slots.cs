using UnityEngine;
using UnityEngine.UIElements;

public class Slots : MonoBehaviour
{
    GameManager GM;
    public float num;
    public bool open = true;
    public Card card;
    bool vis = true;
    float visibility = 1;
    float visibilityChange = 0.02f;
    Vector3 card_goal;
    Vector3 goal1 = new(-75,-150,-1);
    Vector3 goal2 = new(0,-150,-1);
    Vector3 goal3 = new(75,-150,-1);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GM = FindAnyObjectByType<GameManager>();
        if (num == 1) {card_goal = goal1;} if (num == 2) {card_goal = goal2;} if (num == 3) {card_goal = goal3;}
    }

    // Update is called once per frame
    void Update()
    {
        if (vis == true && visibility < 1)
        {
            // Vector4.MoveTowards(GetComponent<SpriteRenderer>().color,new(1,1,1,1),0.1f);
            // if (card != null) {Vector4.MoveTowards(card.GetComponentInChildren<CanvasRenderer>().GetColor(),new(1,1,1,1),0.1f);}

            visibility += visibilityChange;
        }
        if (vis == false && visibility > 0.2)
        {
            //print("DISS");
            // Vector4.MoveTowards(GetComponent<SpriteRenderer>().color,new(1,1,1,0.5f),0.1f);
            // if (card != null) {Vector4.MoveTowards(card.GetComponentInChildren<CanvasRenderer>().GetColor(),new(1,1,1,0.5f),0.1f);}

            visibility -= visibilityChange;
        }

        GetComponent<SpriteRenderer>().color = new(1,1,1,visibility);
        if (card != null && card.isFollowing == false) {card.GetComponentInChildren<CanvasRenderer>().SetColor(new(1,1,1,visibility));}
        if (card != null && card.isFollowing) {card.GetComponentInChildren<CanvasRenderer>().SetColor(new(1,1,1,1));}

        if (card != null && card.goal != transform.position)
        {
            card.goal = card_goal + (Vector3)GM.screen_offset/2;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // GetComponent<SpriteRenderer>().color = new(1,1,1,0.5f);
            // if (card != null) {card.GetComponentInChildren<CanvasRenderer>().SetColor(new(1,1,1,0.5f));}
            vis = false;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // GetComponent<SpriteRenderer>().color = new(1,1,1,1);
            // if (card != null) {card.GetComponentInChildren<CanvasRenderer>().SetColor(new(1,1,1,1));}
            vis = true;
        }
    }
}
