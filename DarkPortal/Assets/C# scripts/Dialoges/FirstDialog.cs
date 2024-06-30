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
        player.speed = 0f; // review(30.06.2024): Кажется, более описательным было бы использование метода player.Freeze()
    }

    private void Update()
    {
        if (triggerDialogue.end && dialogTriggered)
        {
            canvasForFirstItteration.enabled = false;
            player.canvasDefault.enabled = true;
            player.speed = 5f; // review(30.06.2024): Почему именно 5? Выглядит как магическая константа
            dialogTriggered = false;
            triggerDialogue.end = false;
        }
    }
}
