using System.Collections.Generic;
using System.Xml;
using UnityEngine;

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

    public void Draw(List<Card_data> deck, List<Card_data> hand)
    {
        if (hand.Count <= 3)
        {
            if (deck.Count > 0)
            {
                int newCard = (int)GM.RNG(0,deck.Count-1);
                hand.Add(deck[newCard]);
                discard.Add(deck[newCard]);
                deck.Remove(deck[newCard]);
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

            Vector3 pos = new(0,0,0);
            if(GM.Summoner.s1.open == true) {pos = GM.Summoner.s1.transform.position; GM.Summoner.s1.open = false;}
            else if(GM.Summoner.s2.open == true) {pos = GM.Summoner.s2.transform.position; GM.Summoner.s2.open = false;}
            else if(GM.Summoner.s3.open == true) {pos = GM.Summoner.s3.transform.position; GM.Summoner.s3.open = false;}
            else print("MAJOR ERROR: ATTEMPTING TO INSTANTIATE CARD WITH ALL SLOTS FULL");
            Card newHandMember = Instantiate(blank,pos,Quaternion.identity);
            newHandMember.data = hand[(int)GM.RNG(0,hand.Count - 1)];
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
