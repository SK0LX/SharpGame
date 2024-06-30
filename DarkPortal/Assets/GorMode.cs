using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodMode : MonoBehaviour
{
    public Toggle myToggle; // review(30.06.2024): Плохое название

    private void Update()
    {
        GodModeStatus();
    }

    private void GodModeStatus()
    {
        if (myToggle.isOn)
        {
            DataHolder.maxHealth = 10000;
            DataHolder.health = 10000;
        }
        else
        {
            DataHolder.maxHealth = 30;
            DataHolder.health = 30;
        }
    }
}

