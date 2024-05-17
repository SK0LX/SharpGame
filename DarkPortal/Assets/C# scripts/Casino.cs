using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Casino : MonoBehaviour
{
    public Canvas canvasForBTNToCasino;
    public Canvas canvasForCasino;
    public Button btnEnableCasino;

    private void Start()
    {
        canvasForCasino.enabled = false;
        canvasForBTNToCasino.enabled = false;
        btnEnableCasino.onClick.AddListener(TaskOnClick);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasForBTNToCasino.enabled = true;
        }
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasForBTNToCasino.enabled = false;
        }
    }

    private void TaskOnClick()
    {
        canvasForCasino.enabled = true;
    }
}
