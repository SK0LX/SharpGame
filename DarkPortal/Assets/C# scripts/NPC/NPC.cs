using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public Button button;
    public Button buttonInShop;
    public Player player;
    private bool triggeringPlayer;
    private bool shopEnabled;
    public Canvas canvasForButtonShop;
    public Canvas canvasShop;
    [SerializeField] private AudioSource buysmth;

    void Start()
    {
        var btn = button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        var btnInShop = buttonInShop.GetComponent<Button>();
        btnInShop.onClick.AddListener(TaskOnClick);
    }
    
    void Update()
    {
        if (shopEnabled)
        {
            canvasShop.enabled = true;
            canvasForButtonShop.enabled = true;
        }
        else
        {
            canvasShop.enabled = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasForButtonShop.enabled = true;
        }
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasForButtonShop.enabled = false;
        }
    }

    public void TaskOnClick()
    {
        shopEnabled = !shopEnabled;
        player.speed = shopEnabled ? 0 : 5f;
        buysmth.Play();
    }
}