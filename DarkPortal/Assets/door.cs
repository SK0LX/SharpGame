using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class door : MonoBehaviour
{
    public Canvas canvasForTavernaBtn;
    public Button goToTaverna;
    public GameObject positionInTaverna;
    public Player player;


    private void Start()
    {
        goToTaverna.onClick.AddListener(GoToTaverna);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.gameObject.GetComponent<Player>().fight)
        {
            canvasForTavernaBtn.enabled = true;
        }
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasForTavernaBtn.enabled = false;
        }
    }

    private void GoToTaverna()
    {
        player.transform.position = positionInTaverna.transform.position;
    }
}
