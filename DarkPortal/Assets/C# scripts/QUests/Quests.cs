using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quests : MonoBehaviour
{
    public Button firstQuest;
    public TextMeshProUGUI firstQuestTextForButton;
    public Canvas firstQuestCanvas;
    public TextMeshProUGUI firstQuestTextAll;
    
    public GameObject ded;
    
    public Button secondQuest;
    public TextMeshProUGUI secondQuestTextForButton;
    public Canvas secondQuestCanvas;
    public TextMeshProUGUI secondQuestTextAll;

    public GameObject bottle;

    private void Start()
    {
        firstQuest.gameObject.SetActive(false);
        secondQuest.gameObject.SetActive(false);
        firstQuestCanvas.enabled = false;
        secondQuestCanvas.enabled = false;
    }


    public void StartFirstQuest(string textForButton, string[] textForAllInformation)
    {
        firstQuestTextForButton.text = textForButton;
        firstQuest.gameObject.SetActive(true);
        ded.SetActive(true);
        var text = "";
        for (var i = 0; i < textForAllInformation.Length; i++)
        {
            text += $"{i + 1}. {textForAllInformation[i]}\n";
        }
        firstQuestTextAll.text = text;
    }
    
    public void StartSecondQuest(string textForButton, string[] textForAllInformation)
    {
        secondQuestTextForButton.text = textForButton;
        secondQuest.gameObject.SetActive(true);
        bottle.SetActive(true);
        var text = "";
        for (var i = 1; i < textForAllInformation.Length - 1; i++)
        {
            text += $"{i}. {textForAllInformation[i - 1]}\n";
        }

        secondQuestTextAll.text = text;
    }
    
    
    
    public void OnButtonPressedFirstQuest()
    {
        firstQuestCanvas.enabled = !firstQuestCanvas.enabled;
    }
    
    
    public void OnButtonPressedSecondQuest()
    {
        secondQuestCanvas.enabled = !secondQuestCanvas.enabled; 
    }
}
