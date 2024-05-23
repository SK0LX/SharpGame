using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestShopButton : MonoBehaviour
{
    public Canvas canvasForBTNToQuestShop;
    public Button forGoToShopQuest;
    public Canvas canvasForQuestShop;
    public Player player;

    private void Start()
    {
        canvasForBTNToQuestShop.enabled = false;
        forGoToShopQuest.onClick.AddListener(TaskOnClick);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasForBTNToQuestShop.enabled = true;
        }
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasForBTNToQuestShop.enabled = false;
        }
    }

    private void TaskOnClick()
    {
        canvasForQuestShop.enabled = true;
        player.speed = 0f;
    }
    
}
