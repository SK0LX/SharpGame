using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogForMobs : MonoBehaviour
{
    public TextMeshProUGUI name1;
    public TextMeshProUGUI name2;
    public TextMeshProUGUI name3;
    public TextMeshProUGUI bossName;
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public TextMeshProUGUI bossText;
    public Canvas CanvasForMob1;
    public Canvas CanvasForMob2;
    public Canvas CanvasForMob3;
    public Canvas boss;
    public triggetText triggetDialogue1;
    public triggetText triggetDialogue2;
    public triggetText triggetDialogue3;
    public triggetText bossDialog;
    private int countDialog;

    public Player player;

    public void StartMessage()
    {
        switch (countDialog)
        {
            case 0:
                triggetDialogue1.TriggerDialog(CanvasForMob1, name1, text1);
                countDialog++;
                break;
            case 1:
                triggetDialogue2.TriggerDialog(CanvasForMob2, name2, text2);
                countDialog++;
                break;
            case 2:
                triggetDialogue3.TriggerDialog(CanvasForMob3, name3, text3);
                countDialog++;
                break;
            case 3:
                bossDialog.TriggerDialog(boss, bossName, bossText);
                countDialog++;
                break;
        }
    }

    public bool EndDialog()
    {
        if (triggetDialogue1.end)
        {
            CanvasForMob1.enabled = false;
            triggetDialogue1.end = false;
            return true;
        }
        
        if (triggetDialogue2.end)
        {
            CanvasForMob2.enabled = false;
            triggetDialogue2.end = false;
            return true;
        }
        
        if (triggetDialogue3.end)
        {
            CanvasForMob3.enabled = false;
            triggetDialogue3.end = false;
            return true;
        }
        
        if (bossDialog.end)
        {
            boss.enabled = false;
            bossDialog.end = false;
            return true;
        }

        return false;
    }

}
