using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySummoner : MonoBehaviour
{
    public Image cd_circle;
    public Slots s1;
    public Slots s2;
    public Slots s3;
    public List<GameObject> Enemies;
    float spawnTimer;
    float spawnTime = 5;
    GameManager GM;
    public Card_Behavior_Manager CBM;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GM = FindAnyObjectByType<GameManager>();
        CBM = FindAnyObjectByType<Card_Behavior_Manager>();

        spawnTimer = spawnTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            spawnTimer = spawnTime;
            Spawn();
        }
    }

    void Spawn()
    {
        int enemy = (int)(GM.RNG(0,Enemies.Count - 1));

        float xPosNeg;
            if (GM.RNG(0,1) <= 0.5) {xPosNeg = -1;} else {xPosNeg = 1;};
        float yPosNeg;
            if (GM.RNG(0,1) <= 0.5) {yPosNeg = -1;} else {yPosNeg = 1;};

        float xShift = GM.RNG(10,20) * xPosNeg;    //(int)Math.Round((int)Math.Round(GM.RNG(0,1)) - 0.5f);
        float yShift = GM.RNG(10,20) * yPosNeg;    //(int)Math.Round((int)Math.Round(GM.RNG(0,1)) - 0.5f);
        //print("xShift = " + xShift);
        //print("yShift = " + yShift);
        
        GameObject newEnemy = Instantiate(Enemies[enemy],GM.Player.transform.position + new Vector3(xShift,yShift,0),Quaternion.identity,GM.Summoner.transform);
    }
}
