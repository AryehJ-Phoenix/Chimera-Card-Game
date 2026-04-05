using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySummoner : MonoBehaviour
{
    public List<GameObject> Enemies;
    float spawnTimer;
    float spawnTime = 5;
    GameManager GM;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GM = FindAnyObjectByType<GameManager>();

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
        int enemy = (int)Math.Round(GM.RNG(0,1) * (Enemies.Count - 1)); 

        float xPos;
            if (GM.RNG(0,1) <= 0.5) {xPos = -1;} else {xPos = 1;};
        float yPos;
            if (GM.RNG(0,1) <= 0.5) {yPos = -1;} else {yPos = 1;};

        float xShift = GM.RNG(10,20) * xPos;    //(int)Math.Round((int)Math.Round(GM.RNG(0,1)) - 0.5f);
        float yShift = GM.RNG(10,20) * yPos;    //(int)Math.Round((int)Math.Round(GM.RNG(0,1)) - 0.5f);
        //print("xShift = " + xShift);
        //print("yShift = " + yShift);
        
        GameObject newEnemy = Instantiate(Enemies[enemy],GM.Player.transform.position + new Vector3(xShift,yShift,0),Quaternion.identity);
    }
}
