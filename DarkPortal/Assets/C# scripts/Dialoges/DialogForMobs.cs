using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum DialoMob
{
    mob1,
    mob2,
    mob3
}
public class DialogForMobs : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI text;
    public Canvas CanvasForMob1;
    public triggetText triggetDialogue;
    public DialoMob whatsDialog;

    public Player player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void DialogFight()
    {
        CanvasForMob1.enabled = true;
        player.speed = 0f;
        
        switch (whatsDialog)
        {
            case DialoMob.mob1:
                triggetDialogue.TriggerDialog(CanvasForMob1, name, text);
                break;
        }

        CanvasForMob1.enabled = true;
        player.speed = 0f;
    }

    public bool isEnd()
    {
        return triggetDialogue.end;
    }
}
