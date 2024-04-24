using System.Collections;
using System.Collections.Generic;
using C__scripts;
using UnityEngine;
using System;
using Random = System.Random;


enum EntityForFight
{
    Mob = 1,
    Player = 2
}
public class Fight : MonoBehaviour
{
    public GameObject mob;
    public GameObject player;
    
    private bool isPlayerTurn = true;

    
    // Start is called before the first frame update
    void Start()
    {
        RandomFight();
    }

    // Update is called once per frame
    void Update()
    { 
        RandomMashine();
    }


    private void RandomFight()
    {
        var rnd = new Random();
        isPlayerTurn = rnd.Next(1, 3) == 1;
    }


    private void RandomMashine()
    {
        if (isPlayerTurn)
        {
            // Логика действий игрока
            Debug.Log("Ход игрока");
        }
        else
        {
            // Логика действий противника
            Debug.Log("Ход противника");
        }

        isPlayerTurn = !isPlayerTurn; // Переключение хода
    }

}
