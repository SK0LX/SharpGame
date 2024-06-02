using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirstDialog : MonoBehaviour
{
    public Canvas canvasForFirstItteration;
    public TriggetText triggerDialogue;
    public Player player;
    private bool dialogTriggered = true;

    public TextMeshProUGUI name;
    public TextMeshProUGUI text;
    private void Start()
    {
        canvasForFirstItteration.enabled = true;
        player.canvasDefault.enabled = false;
        triggerDialogue.TriggerDialog(canvasForFirstItteration, name, text);
        player.speed = 0f;
    }

    private void Update()
    {
        if (triggerDialogue.end && dialogTriggered)
        {
            canvasForFirstItteration.enabled = false;
            player.canvasDefault.enabled = true;
            player.speed = 5f;
            dialogTriggered = false;
            triggerDialogue.end = false;
        }
    }
}
