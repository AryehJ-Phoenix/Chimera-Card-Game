using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public List<Card> deck = new List<Card>();
    public List<Card> player_deck = new List<Card>();
    public List<Card> ai_deck = new List<Card>();
    public List<Card> player_hand = new List<Card>();
    public List<Card> ai_hand = new List<Card>();
    public List<Card> discard_pile = new List<Card>();

    public PlayerControl Player = null;
    public Slots slot_1 = null;
    public Slots slot_2 = null;
    public Slots slot_3 = null;
    Slots obj;
    private void Awake()
    {
        if (gm != null && gm != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gm = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            Player = FindAnyObjectByType<PlayerControl>();
            print(Player);
        }
        if(Player.s1 != null){slot_1=Player.s1;}
        if(Player.s2 != null){slot_2=Player.s2;}
        if(Player.s3 != null){slot_3=Player.s3;}
    }

    void Deal()
    {

    }

    void Shuffle()
    {

    }

    void AI_Turn()
    {

    }


    public void OnStartPressed()
    {
        SceneManager.LoadScene("Game");
        
    }
    
    public float RNG(float min,float max)
    {
        return UnityEngine.Random.Range(min,max);
    }
}