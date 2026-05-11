using System.Text.RegularExpressions;
using UnityEngine;

public class Card_Behavior_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindCardEffect(Card_data card, GameObject origin, Vector2 direction)
    {
        if (card.card_name == "Headbut") {Headbut(origin,direction);}
        if (card.card_name == "Punch") {Punch(origin,direction);}
    }

    void Headbut(GameObject origin, Vector2 direction)
    {

        origin.transform.Translate(new(0,0,0));
    }

    void Punch(GameObject origin, Vector2 direction)
    {
        
    }
}
