using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class FortuneWhheel : MonoBehaviour
{
    private int numberOfTurnes; //кол-во оборотов
    private int WhatWeWin;
    
    private float speed; // скорость вращения и затухания
    
    private bool canWeTurn;

    private string winningText;
    private string totalChoiseText;

    
    public Player player;
    public TextMeshProUGUI totalPlayerCoins;

    public Button btnRed;
    public Button btnGreen;
    public Button btnBlack;
    public Button btnUp;
    public Button btnDown;
    
    
    private int totalBet;
    private int currentMultiplier;
    
    public TextMeshProUGUI totalBetText;
    public TextMeshProUGUI winOrLose;
    public TextMeshProUGUI totalWin;

    private void Start()
    {
        btnRed.onClick.AddListener(PressRed);
        btnGreen.onClick.AddListener(PressGreen);
        btnBlack.onClick.AddListener(PressBlack);
        btnUp.onClick.AddListener(PressBtnUp);
        btnDown.onClick.AddListener(PressBtnDown);
        canWeTurn = true;
        winOrLose.text = "Проверим твою удачу!";
        totalBetText.text = "0";
    }

    private void Update()
    {
        totalPlayerCoins.text = player.inventory.coins.ToString();
        var rotationSpeed = 10f;
        if (canWeTurn)
        {
            transform.Rotate(0,0, rotationSpeed * Time.deltaTime);
        }
    }


    private IEnumerator TurnTheWheel()
    {
        canWeTurn = false;
        numberOfTurnes = Random.Range(70, 100);
        speed = 0.01f;

        for (var i = 0; i < numberOfTurnes; i++)
        {
            transform.Rotate(0, 0, 12f);
            if (i > Mathf.RoundToInt(numberOfTurnes * 0.5f))
            {
                speed = 0.02f;
            }
            
            if (i > Mathf.RoundToInt(numberOfTurnes * 0.7f))
            {
                speed = 0.07f;
            }
            
            if (i > Mathf.RoundToInt(numberOfTurnes * 0.9f))
            {
                speed = 0.09f;
            }

            yield return new WaitForSeconds(speed);
        }
        

        WhatWeWin = Mathf.RoundToInt(transform.rotation.eulerAngles.z) / 12;

        switch (WhatWeWin)
        {
            case 0:
                winningText = "Зеленое";
                break;
            case 1:
                winningText = "Черное";
                break;
            case 2:
                winningText = "Красное";
                break;
            case 3:
                winningText = "Черное";
                break;
            case 4:
                winningText = "Красное";
                break;
            case 5:
                winningText = "Черное";
                break;
            case 6:
                winningText = "Красное";
                break;
            case 7:
                winningText = "Черное";
                break;
            case 8:
                winningText = "Красное";
                break;
            case 9:
                winningText = "Черное";
                break;
            case 10:
                winningText = "Красное";
                break;
            case 11:
                winningText = "Черное";
                break;
            case 12:
                winningText = "Красное";
                break;
            case 13:
                winningText = "Черное";
                break;
            case 14:
                winningText = "Красное";
                break;
            case 15:
                winningText = "Черное";
                break;
            case 16:
                winningText = "Красное";
                break;
            case 17:
                winningText = "Черное";
                break;
            case 18:
                winningText = "Красное";
                break;
            case 19:
                winningText = "Черное";
                break;
            case 20:
                winningText = "Красное";
                break;
            case 21:
                winningText = "Черное";
                break;
            case 22:
                winningText = "Красное";
                break;
            case 23:
                winningText = "Черное";
                break;
            case 24:
                winningText = "Красное";
                break;
            case 25:
                winningText = "Черное";
                break;
            case 26:
                winningText = "Красное";
                break;
            case 27:
                winningText = "Черное";
                break;
            case 28:
                winningText = "Красное";
                break;
            case 29:
                winningText = "Черное";
                break;
        }
        totalWin.text = $"Выпало: {winningText}"; 
        CheckWinOrLose();
        
        yield return new WaitForSeconds(3f);
        canWeTurn = true;
    }

    private void CheckWinOrLose()
    {
        if (totalChoiseText == winningText)
        {
            winOrLose.text = $"Ты Выйграл {totalBet * (currentMultiplier - 1)} монет!";
            player.inventory.coins += totalBet * (currentMultiplier - 1);
        }
        else
        {
            winOrLose.text = $"Ты проиграл {totalBet} монет!";
            player.inventory.coins -= totalBet;
        }
    }

    private void PressBtnUp()
    {
        totalBet += 10;
        if (totalBet > 100)
            totalBet = 100;
        totalBetText.text = totalBet.ToString();

    }
    
    private void PressBtnDown()
    {
        totalBet -= 10;
        if (totalBet < 0)
            totalBet = 0;
        totalBetText.text = totalBet.ToString();
    }


    private void PressGreen()
    {
        if (canWeTurn)
        {
            totalChoiseText = "Зеленое";
            currentMultiplier = 40;
        }

        RollWheel();
    }
    
    private void PressBlack()
    {
        if (canWeTurn)
        {
            totalChoiseText = "Черное";
            currentMultiplier = 2;
        }

        RollWheel();
    }
    
    private void PressRed()
    {
        if (canWeTurn)
        {
            totalChoiseText = "Красное";
            currentMultiplier = 2;
        }

        RollWheel();
    }
    
    private void RollWheel()
    {
        if (canWeTurn && player.inventory.coins >= totalBet)
        {
            //вращаем
            StartCoroutine(TurnTheWheel());
        }
        else
        {
            print("Недостаточно средств");
        }
    }
}
