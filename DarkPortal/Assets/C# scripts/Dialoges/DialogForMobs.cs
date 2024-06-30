using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogForMobs : MonoBehaviour
{
    public TextMeshProUGUI name1;
    public TextMeshProUGUI bossName;
    public TextMeshProUGUI text1;
    public TextMeshProUGUI bossText;
    public Canvas CanvasForMob1;
    public Canvas boss;
    public TriggetText triggetDialogue1;
    public TriggetText triggetDialogue2;
    public TriggetText triggetDialogue3;
    public TriggetText bossDialog;
    private int countDialog;
    private Dialog[] dialogs;

    public Player player;

    public void Start()
    {
        dialogs = new[]
        {
            new Dialog(triggetDialogue1, CanvasForMob1, name1, text1, disableCanvasOnSkip: true),
            new Dialog(triggetDialogue2, CanvasForMob1, name1, text1),
            new Dialog(triggetDialogue3, CanvasForMob1, name1, text1),
            new Dialog(bossDialog, boss, bossName, bossText, disableCanvasOnSkip: true),
        };
    }

    public void StartMessage()
    {
        switch (countDialog)
        {
            case 0:
                triggetDialogue1.TriggerDialog(CanvasForMob1, name1, text1);
                countDialog++; // review(30.06.2024): Как минимум, увеличение счетчика можно вынести из switch
                break;
            case 1:
                triggetDialogue2.TriggerDialog(CanvasForMob1, name1, text1);
                countDialog++;
                break;
            case 2:
                triggetDialogue3.TriggerDialog(CanvasForMob1, name1, text1);
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
            CanvasForMob1.enabled = false; // review(30.06.2024): Не совсем понятно, почему только в этом случае и при боссе отключается канвас
            triggetDialogue1.end = false;
            return true;
        }
        
        if (triggetDialogue2.end)
        {
            triggetDialogue2.end = false;
            return true;
        }
        
        if (triggetDialogue3.end)
        {
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

    // review(30.06.2024): Примерно такой код хотелось бы видеть вместо захардкоженных значений. Он более удобный и расширяемый
    public void StartMessage_Refactored()
    {
        if (countDialog >= dialogs.Length)
            return;

        var dialog = dialogs[countDialog];

        dialog.Run();
        
        countDialog++;
    }

    public bool EndDialog_Refactored()
    {
        var skipRequiredDialog = dialogs.FirstOrDefault(x => x.SkipRequired);

        if (skipRequiredDialog is null)
            return false;
        
        skipRequiredDialog.AcceptSkip();
        return true;
    }

    private class Dialog
    {
        private readonly TriggetText trigger;
        private readonly Canvas canvas;
        private readonly TextMeshProUGUI name;
        private readonly TextMeshProUGUI text;
        private readonly bool disableCanvasOnSkip;

        public Dialog(TriggetText trigger, Canvas canvas, TextMeshProUGUI name, TextMeshProUGUI text, bool disableCanvasOnSkip = false)
        {
            this.trigger = trigger;
            this.canvas = canvas;
            this.name = name;
            this.text = text;
            this.disableCanvasOnSkip = disableCanvasOnSkip;
        }

        public bool SkipRequired => trigger.end;

        public void AcceptSkip()
        {
            if (disableCanvasOnSkip)
                canvas.enabled = false;
            trigger.end = false;
        }

        public void Run() => trigger.TriggerDialog(canvas, name, text);
    }
}
