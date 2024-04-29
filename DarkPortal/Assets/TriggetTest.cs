using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class TriggetTest : MonoBehaviour
{
    public Canvas canvas;
    public static bool fight;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.enabled = !canvas.enabled;
            fight = true;
        }
    }
}
