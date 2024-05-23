using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControllerQuests : MonoBehaviour
{
    public Canvas canvasShopForQuests;
    
    public Button firstQuest;
    public TextMeshProUGUI firstQuestTextForButton;
    public Button secondQuest;
    public TextMeshProUGUI secondQuestTextForButton;
    public Player player;

    public Button back;
    private void Start()
    {
        firstQuest.onClick.AddListener(ActivateFirstQuest);
        secondQuest.onClick.AddListener(ActivateSecondQuest);
        back.onClick.AddListener(ExitQuest);
    }
    
    private void ActivateFirstQuest()
    {
        var quest = player.GetComponent<Quests>();
        quest.StartFirstQuest("Спасти деда", new []{"Бедный дедушка потерялся в этом странном и непонятном мире. Нужно его найти",
            "Он мог попасть в беду, к монстрам. Нужно победить мобов",
            "Спасите деда"}); 
        firstQuestTextForButton.text = "Квест взят";
        firstQuest.enabled = false;
    }
    
    private void ActivateSecondQuest()
    {
        var quest = player.GetComponent<Quests>();
        quest.StartSecondQuest("Найти пропавшую бутылку", new []{"Найти бутылку"});
        secondQuestTextForButton.text = "Квест взят";
        secondQuest.enabled = false;
    }
    
    private void ExitQuest()
    {
        canvasShopForQuests.enabled = !canvasShopForQuests.enabled;
        player.speed = 5f;
    }
}
