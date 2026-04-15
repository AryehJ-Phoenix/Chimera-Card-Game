using UnityEngine;
using UnityEngine.UIElements;

public class Slots : MonoBehaviour
{
    GameManager GM;
    public float num;
    public bool open = true;
    public Card card;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GM = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = new(1,1,1,0.5f);
            if (card != null) {card.GetComponentInChildren<CanvasRenderer>().SetColor(new(1,1,1,0.5f));}
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = new(1,1,1,1);
            if (card != null) {card.GetComponentInChildren<CanvasRenderer>().SetColor(new(1,1,1,1));}
        }
    }
}
