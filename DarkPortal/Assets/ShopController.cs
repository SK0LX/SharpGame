using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public Player player;
    private int coinsPLayer;
    
    public Button buttonForSmallHP;
    public Button buttonForBigHP;
    public Button buttonForDecorationHP;
    public bool buy1HP;
    public Button buttonForDecorationDeterity;
    public bool buy2Deterity;
    [SerializeField] private AudioSource noMonie;
    void Start()
    {
        buttonForSmallHP.GetComponent<Button>().onClick.AddListener(HpSmall);
        
        buttonForBigHP.GetComponent<Button>().onClick.AddListener(HpBig);
        
        buttonForDecorationHP.GetComponent<Button>().onClick.AddListener(DecorationHP);
        
        buttonForDecorationDeterity.GetComponent<Button>().onClick.AddListener(DecorationDexterity);
    }

    private void Update()
    {
        if (buy1HP)
        {
            
            buttonForDecorationHP.enabled = false;
        }

        if (buy2Deterity)
        {

            buttonForDecorationDeterity.enabled = false;
        }
    }

    void HpSmall()
    {
        if (player.inventory.coins >= 7)
        {
            player.inventory.coins -= 7;
            player.inventory.hpSmallBottel += 1;
        }
        else
        {
            noMonie.Play();
        }
    }

    void HpBig()
    {
        if (player.inventory.coins >= 25)
        {
            player.inventory.coins -= 25;
            player.inventory.hpBigBottel += 1;
        }
        else
        {
            noMonie.Play();
        }
    }

    void DecorationHP()
    {
        if (player.inventory.coins >= 25)
        {
            player.inventory.coins -= 25;
            player.HP.DecorationBoost(10);
            buy1HP = true;
            player.inventory.decorationFirstBool = true;
        }
        else
        {
            noMonie.Play();
        }
    }
    
    void DecorationDexterity()
    {
        if (player.inventory.coins >= 25)
        {
            player.inventory.coins -= 25;
            player.inventory.dexterity += 5;
            buy2Deterity = true;
            player.inventory.decorationSecondBool = true;
        }
        else
        {
            noMonie.Play();
        }
    }
}
