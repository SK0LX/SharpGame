using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class TriggetTest : MonoBehaviour
{
    public Canvas canvas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Thread.Sleep(500);
        canvas.enabled = !canvas.enabled;
        transform.position += new Vector3(0, 50, 0);
    }
}
