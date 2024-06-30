using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class FortuneWheel : MonoBehaviour
{
    private int numberOfTurnes; //кол-во оборотов
    private int WhatWeWin;
    
    private float speed; // скорость вращения и затухания
    
    private bool canWeTurn;

    private string winningText;
    private string choice;

    
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

    [SerializeField] private AudioSource melstroi;
    [SerializeField] private AudioSource fail;
    [SerializeField] private AudioSource win;
    [SerializeField] private AudioSource million;
    [SerializeField] private AudioSource noMonie;

    private void Start()
    {
        btnRed.onClick.AddListener(PressRed);
        btnGreen.onClick.AddListener(PressGreen);
        btnBlack.onClick.AddListener(PressBlack);
        btnUp.onClick.AddListener(PressBtnUp);
        btnDown.onClick.AddListener(PressBtnDown);
        canWeTurn = true;
        winOrLose.text = "Проверим твою удачу!";
        totalBetText.text = "10";
        totalBet = 10;
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
        numberOfTurnes = Random.Range(200, 300);
        speed = 0.01f;
        melstroi.Play();
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
        
        melstroi.Stop();
        WhatWeWin = Mathf.RoundToInt(transform.rotation.eulerAngles.z) / 12; // review(30.06.2024): Почему что мы выиграли, а не игрок?

        if (WhatWeWin == 0)
            winningText = "Зеленое"; // review(30.06.2024): Плохо использовать строки вот так. Стоило предсоздать различные ставки и использовать choice из поля BetChoice
        else switch (WhatWeWin % 2)
        {
            case 1:
                winningText = "Черное";
                break;
            case 0 when WhatWeWin != 0: // review(30.06.2024): Проверка избыточна -- мы в else
                winningText = "Красное";
                break;
        }

        totalWin.text = $"Выпало: {winningText}"; 
        CheckWinOrLose();
        
        yield return new WaitForSeconds(3f);
        canWeTurn = true;
    }

    private void CheckWinOrLose()
    {
        if (choice == winningText)
        {
            if (winningText == "Зеленое")
                million.Play();
            else
                win.Play();
            winOrLose.text = $"Ты Выйграл {totalBet * (currentMultiplier - 1)} монет!";
            player.inventory.coins += totalBet * (currentMultiplier - 1);
            
        }
        else
        {
            fail.Play();
            winOrLose.text = $"Ты проиграл {totalBet} монет!";
            player.inventory.coins -= totalBet;
            
        }
    }

    private void PressBtnUp()
    {
        totalBet = Math.Min(totalBet + 10, 100);
        totalBetText.text = totalBet.ToString();

    }
    
    private void PressBtnDown()
    {
        totalBet = Math.Max(totalBet - 10, 10);
        totalBetText.text = totalBet.ToString();
    }


    // review(30.06.2024): Во всех кнопках почти один и тот же код. Т.к. у них одно использование, я бы выделил просто один метод и переиспользовал бы его

    private void Press(BetChoice betChoice)
    {
        if (canWeTurn)
        {
            MakeBet(betChoice);
        }
    }

    private void PressGreen()
    {
        if (canWeTurn)
        {
            BetChoice bet = new BetChoice("Зеленое", 40);
            MakeBet(bet);
        }
    }

    private void PressBlack()
    {
        if (canWeTurn)
        {
            BetChoice bet = new BetChoice("Черное", 2);
            MakeBet(bet);
        }
    }

    private void PressRed()
    {
        if (canWeTurn)
        {
            BetChoice bet = new BetChoice("Красное", 2);
            MakeBet(bet);
        }
    }

    private void MakeBet(BetChoice bet)
    {
        choice = bet.choice;
        currentMultiplier = bet.currentMultiplier;
        RollWheel();
    }

    
    private void RollWheel()
    {
        if (canWeTurn && player.inventory.coins >= totalBet)
        {
            //вращаем
            StartCoroutine(TurnTheWheel());
        }

        if (player.inventory.coins < totalBet)
        {
            noMonie.Play();
        }
    }

    private struct BetChoice
    {
        // review(30.06.2024): Количество Choice ограничено. Я бы еще добавил поля ниже

        public static readonly BetChoice Green = new("Зеленый", 40);
        public static readonly BetChoice Red = new("Зеленый", 40);
        public static readonly BetChoice Black = new("Зеленый", 40);

        // review(30.06.2024): Почему поля изменяемые?
        public string choice;
        public int currentMultiplier;

        public BetChoice(string choice, int multiplier)
        {
            this.choice = choice;
            currentMultiplier = multiplier;
        }
    }

}
