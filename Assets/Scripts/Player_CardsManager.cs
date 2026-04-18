using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_CardsManager : MonoBehaviour
{
    public List<Card_data> deck = new();
    public List<Card_data> hand = new();
    public List<Card_data> discard = new();
    [SerializeField] Card blank;
    GameManager GM;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GM = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Input(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Draw(deck,hand);
        }
    }

    public void Draw(List<Card_data> deck, List<Card_data> hand)
    {
        if (hand.Count < 3)
        {
            if (deck.Count > 0)
            {
                int newCard = (int)GM.RNG(0,deck.Count-1);
                hand.Add(deck[newCard]);
                deck.Remove(deck[newCard]);

                Slots slot = null;

                Vector3 pos = new(0,0,0);
                if(GM.Summoner.s1.open == true) {pos = new(-75,-150,-1); GM.Summoner.s1.open = false; slot = GM.Summoner.s1;}
                else if(GM.Summoner.s2.open == true) {pos = new(0,-150,-1); GM.Summoner.s2.open = false; slot = GM.Summoner.s2;}
                else if(GM.Summoner.s3.open == true) {pos = new(75,-150,-1); GM.Summoner.s3.open = false; slot = GM.Summoner.s3;}
                
                Card newHandMember = Instantiate(blank,new(0,-250,0),Quaternion.identity,GM.canvas.transform);
                newHandMember.data = hand[(int)GM.RNG(0,hand.Count - 1)];
                newHandMember.name = newHandMember.data.card_name + " Card (Slot " + slot.num + ")";
                Vector3 offset; if (GM.mac) {offset = GM.mac_offset;} else {offset = GM.laptop_offset;}
                newHandMember.transform.Translate(offset);
                newHandMember.goal = pos + offset;
                if (GM.mac) {newHandMember.transform.localScale = GM.mac_scale;} else {newHandMember.transform.localScale = GM.laptop_scale;}
                if (slot != null) {slot.card = newHandMember;}
                newHandMember.offset = offset;
                newHandMember.slot = slot;
            }
            else
            {
                RenewDeck();
            }

            Draw(deck,hand);
        }

        else 
        {
            print("HAND SIZE AT MAX. CANNOT ADD MORE");

            // Vector3 pos = new(0,0,0);
            // if(GM.Summoner.s1.open == true) {pos = new(-95,-190,-1); GM.Summoner.s1.open = false;}
            // else if(GM.Summoner.s2.open == true) {pos = new(0,-190,-1); GM.Summoner.s2.open = false;}
            // else if(GM.Summoner.s3.open == true) {pos = new(95,-190,-1); GM.Summoner.s3.open = false;}
            // else print("MAJOR ERROR: ATTEMPTING TO INSTANTIATE CARD WITH ALL SLOTS FULL");
            // print("NEW CARD POSITION: " + pos);
            
            // Card newHandMember = Instantiate(blank,pos,Quaternion.identity,GM.canvas.transform);
            // newHandMember.data = hand[(int)GM.RNG(0,hand.Count - 1)];
            // newHandMember.transform.SetParent(GM.canvas.transform);
        }
    }

    void RenewDeck()
    {
        for (int i = discard.Count; i > 0; i--)
        {
            int newCard = (int)GM.RNG(0,discard.Count - 1);
            deck.Add(discard[newCard]);
            discard.Remove(discard[newCard]);
        }
    }
}
