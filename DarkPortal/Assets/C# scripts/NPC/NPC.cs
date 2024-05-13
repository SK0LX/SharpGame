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
    public Canvas CanvasForDialog;
    [SerializeField] private AudioSource buysmth;
    public triggetText triggetDialogue;
    private bool beginDilogue;

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
        if (!beginDilogue)
        {
            canvasShop.enabled = true;
            canvasForButtonShop.enabled = true;
        }
        beginDilogue = !beginDilogue;
        shopEnabled = !shopEnabled;
        player.speed = shopEnabled ? 0 : 5f;
        buysmth.Play();
    }
}