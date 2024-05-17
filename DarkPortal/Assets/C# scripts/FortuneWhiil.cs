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

    public TextMeshProUGUI winningText;

    private void Start()
    {
        canWeTurn = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canWeTurn)
        {
            //вращаем
            StartCoroutine(TurnTheWheel());
        }
    }
    
    private IEnumerator TurnTheWheel()
    {
        canWeTurn = false;
        numberOfTurnes = Random.Range(40, 60);
        speed = 0.01f;

        for (var i = 0; i < numberOfTurnes; i++)
        {
            transform.Rotate(0, 0, 15f);
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

        if (Mathf.RoundToInt(transform.eulerAngles.z) % 30 != 0)
        {
            transform.Rotate(0, 0, 15f);
        }

        WhatWeWin = Mathf.RoundToInt(transform.eulerAngles.z + 14);

        switch (WhatWeWin)
        {
            case 0:
                winningText.text = "Зеленое";
                break;
            case 30:
                winningText.text = "Красное";
                break;
            case 60:
                winningText.text = "Черное";
                break;
            case 90:
                winningText.text = "Красное";
                break;
            case 120:
                winningText.text = "Черное";
                break;
            case 150:
                winningText.text = "Красное";
                break;
            case 180:
                winningText.text = "Черное";
                break;
            case 210:
                winningText.text = "Красное";
                break;
            case 240:
                winningText.text = "Черное";
                break;
            case 270:
                winningText.text = "Красное";
                break;
            case 300:
                winningText.text = "Черное";
                break;
            case 330:
                winningText.text = "Красное";
                break;
        }

        canWeTurn = true;
    }
}
