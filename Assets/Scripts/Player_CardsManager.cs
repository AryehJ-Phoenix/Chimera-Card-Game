using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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
        Player = GetComponentInParent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        // float angle = Vector2.Angle((Vector2)Player.transform.localPosition,(Vector2)GM.mousePosIRL - (Vector2)Player.transform.position);
        // print(angle);
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
        if (card.card_name == "Arrow") {Arrow(card);}
    }

    void Headbut(Card_data card)
    {
        Rigidbody2D rigidbody = Player.GetComponent<Rigidbody2D>();

        Vector2 angle = (GM.mousePosIRL - Player.transform.position);
        angle.Normalize();
        Player.oldSpeed = rigidbody.linearVelocity;
        rigidbody.linearVelocity = new(0,0);
        Player.timeUntilMove = 0.25f;
        rigidbody.linearVelocity = 4*card.range*angle;

        //GM.Player.transform.Translate(angle*card.range * -1);
    }

    void Punch(Card_data card)
    {
        Damager damageCapsule = Player.damageCapsule;

        Vector2 dir = (GM.mousePosIRL - Player.transform.position);
        dir.Normalize();
        float angle = Vector2.Angle((Vector2)Player.transform.position,(Vector2)GM.mousePosIRL);
        float nangle = Mathf.Atan2(GM.mousePosIRL.y-Player.transform.position.y,GM.mousePosIRL.x-Player.transform.position.x) * Mathf.Rad2Deg;
        print("Punch angle: " + nangle);
        print("Punch direction: " + dir);
        

        Damager damager = Instantiate(damageCapsule,transform.position + (Vector3)dir*card.range,Quaternion.identity,Player.transform);
        // damager.transform.Translate(dir*card.range);
        damager.transform.Rotate(0,0,nangle);
        print("Punch pos: " + damager.transform.position);
        damager.damage = card.damage;
        damager.lifetime = 0.5f;
        Player.GetComponent<Rigidbody2D>().linearVelocity = new(0,0);
        Player.timeUntilMove = 0.5f;
        Player.GetComponent<Rigidbody2D>().linearVelocity = new(0,0);
    }

    void Arrow(Card_data card)
    {
        Damager damageCapsule = Player.damageCapsule;

        Vector2 dir = (GM.mousePosIRL - Player.transform.position);
        dir.Normalize();
        float nangle = Mathf.Atan2(GM.mousePosIRL.y-Player.transform.position.y,GM.mousePosIRL.x-Player.transform.position.x) * Mathf.Rad2Deg;
        Damager damager = Instantiate(damageCapsule,transform.position + (Vector3)dir,Quaternion.identity,Player.transform);
        damager.AddComponent<Moving>();
        damager.GetComponent<Moving>().speed = card.range*5;
        damager.GetComponent<Moving>().dir = dir;
        damager.transform.Rotate(0,0,nangle+90);
        damager.damage = card.damage;
        
        damager.lifetime = 1f;
    }
}
