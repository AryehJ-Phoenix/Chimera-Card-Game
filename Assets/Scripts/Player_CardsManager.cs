using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_CardsManager : MonoBehaviour
{
    public List<Card_data> deck = new();
    public List<Card_data> hand = new();
    public List<Card_data> discard = new();
    [SerializeField] Card blank;
    GameManager GM;
    PlayerControl Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GM = FindAnyObjectByType<GameManager>();
        Player = GM.Player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Input(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Draw();
            print("SACE");
        }
    }

    public void Draw()
    {
        int drawUpTo = 3 - hand.Count;
        if (deck.Count + discard.Count < drawUpTo) {drawUpTo = deck.Count + discard.Count;}
        if (hand.Count  >= 3) {drawUpTo = 0;}
        // print("Drawing up to " + drawUpTo);

        for (int i = 3 - drawUpTo; i < 3; i++)
        {
            if (deck.Count <= 0)
            {
                RenewDeck();
            }
            int newCard = (int)GM.RNG(0,deck.Count-1);
            hand.Add(deck[newCard]);

            Slots slot = null;

            Vector3 pos = new(0,0,0);
            if(GM.Summoner.s1.open == true) {GM.Summoner.s1.open = false; slot = GM.Summoner.s1;}
            else if(GM.Summoner.s2.open == true) {GM.Summoner.s2.open = false; slot = GM.Summoner.s2;}
            else if(GM.Summoner.s3.open == true) {GM.Summoner.s3.open = false; slot = GM.Summoner.s3;}
            
            Card newHandMember = Instantiate(blank,new(0,-250,0),Quaternion.identity,GM.canvas.transform);
            newHandMember.data = deck[newCard];
            newHandMember.name = newHandMember.data.card_name + " Card (Slot " + slot.num + ")";
            newHandMember.transform.Translate(GM.screen_offset/2);
            //newHandMember.goal = pos + (Vector3)GM.screen_offset/2;
            if (GM.mac) {newHandMember.transform.localScale = GM.mac_scale;} else {newHandMember.transform.localScale = GM.laptop_scale;}
            if (slot != null) {slot.card = newHandMember;}
            newHandMember.slot = slot;

            deck.Remove(deck[newCard]);
        }
    }

    void RenewDeck()
    {
        // print("SHUFFLE");
        for (int i = discard.Count; i > 0; i--)
        {
            int newCard = (int)GM.RNG(0,discard.Count - 1);
            deck.Add(discard[newCard]);
            discard.Remove(discard[newCard]);
        }
    }





    public void FindCardEffect(Card_data card)
    {
        //print("Detected card name " + card.card_name);
        if (card.card_name == "Headbut") {Headbut(card);}
        if (card.card_name == "Punch") {Punch(card);}
    }

    void Headbut(Card_data card)
    {
        Rigidbody2D rigidbody = GM.Player.GetComponent<Rigidbody2D>();
        print("PLAYED HEADBUT");

        Vector3 angle = (GM.mousePosIRL - GM.Player.transform.position);
        angle.Normalize();
        rigidbody.linearVelocity = new(0,0);
        GM.Player.timeUntilMove = 0.5f;
        rigidbody.linearVelocity = 4*card.range*angle;

        //GM.Player.transform.Translate(angle*card.range * -1);
    }

    void Punch(Card_data card)
    {
        print("PLAYED PUNCH");
    }
}
