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
        FillQuest(textForAllInformation);
    }
    

    public void StartSecondQuest(string textForButton, string[] textForAllInformation)
    {
        secondQuestTextForButton.text = textForButton;
        secondQuest.gameObject.SetActive(true);
        bottle.SetActive(true);
        FillQuest(textForAllInformation);
    }
    
    
    
    public void OnButtonPressedFirstQuest()
    {
        firstQuestCanvas.enabled = !firstQuestCanvas.enabled;
    }
    
    
    public void OnButtonPressedSecondQuest()
    {
        secondQuestCanvas.enabled = !secondQuestCanvas.enabled; 
    }

    public void FinishQuest(int number)
    {
        var text = "Выполнено";
        if (number == 1)
            firstQuestTextForButton.text = text;
        else if (number == 2)
            secondQuestTextForButton.text = text;
        FindObjectOfType<CanvasControllerQuests>().FinishQuest(number);
    }
    
    private void FillQuest(string[] textForAllInformation)
    {
        var text = new StringBuilder();
        for (var i = 0; i < textForAllInformation.Length; i++)
        {
            text.Append($"{i + 1}. {textForAllInformation[i]}\n");
        }
        firstQuestTextAll.text = text.ToString();
    }
}
