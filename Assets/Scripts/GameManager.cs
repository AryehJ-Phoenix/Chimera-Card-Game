using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public EnemySummoner Summoner = null;
    public Player_CardsManager CM = null;
    public Canvas canvas = null;
    public Slots slot_1 = null;
    public Slots slot_2 = null;
    public Slots slot_3 = null;
    Slots obj;
    public Vector2 mac_offset = new(528.25f,226.25f);
    public Vector2 laptop_offset = new(597.75f,236.6296f);
    public Vector3 mac_scale = new(0.2f,0.2f,0.2f);
    public Vector3 laptop_scale = new(0.25f,0.25f,0.25f);
    public bool mac = true;
    public Vector3 mousePos;
    public Vector3 mousePosIRL;
    public Vector2 screen_offset;
    public float drawTime = 4;
    float drawTimer;
    Image cd_circle;

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
        drawTimer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null) {Player = FindAnyObjectByType<PlayerControl>();}
        if (Summoner == null) {Summoner = FindAnyObjectByType<EnemySummoner>();}
        if (canvas == null) {canvas = FindAnyObjectByType<Canvas>();}
        if (CM == null) {CM = FindAnyObjectByType<Player_CardsManager>();}

        if (Summoner.s1 != null) {slot_1 = Summoner.s1;}
        if (Summoner.s2 != null) {slot_2 = Summoner.s2;}
        if (Summoner.s3 != null) {slot_3 = Summoner.s3;}

        // if (mac) {mousePos = Mouse.current.position.ReadValue() - mac_offset;}
        // else {mousePos = Mouse.current.position.ReadValue() - laptop_offset;}
        screen_offset = canvas.renderingDisplaySize;
        mousePos = Mouse.current.position.ReadValue() - screen_offset/2;
        mousePosIRL = Camera.main.ScreenToWorldPoint(new(Mouse.current.position.ReadValue().x,Mouse.current.position.ReadValue().y,0));

        if (Summoner != null && cd_circle == null) {cd_circle = Summoner.cd_circle;}
        if (cd_circle != null) {cd_circle.fillAmount = drawTimer;}

        drawTimer -= Time.deltaTime/drawTime;

        if (drawTimer <= 0) {if (CM != null) {CM.Draw();} drawTimer = 1f;}
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