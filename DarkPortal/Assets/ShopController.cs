using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public Player player;

    // review(30.06.2024): Почему бы не выделить для разных вещей общую логику и назвать это ShopItem, а уже внутри себя он как-то бы понимал, что нужно делать при нажатии кнопки?
    public Button buttonForSmallHP;
    public Button buttonForBigHP;
    public Button buttonForDecorationHP;
    private bool buyBoostForHp;
    public Button buttonForDecorationDeterity;
    private bool buyBoostForDexterity; 
    [SerializeField] private AudioSource noMoneyAudioSource;
    [SerializeField] private AudioSource buyItem;
    void Start()
    {
        buttonForSmallHP.onClick.AddListener(HpSmall);
        
        buttonForBigHP.onClick.AddListener(HpBig);
        
        buttonForDecorationHP.onClick.AddListener(DecorationHP);
        
        buttonForDecorationDeterity.onClick.AddListener(DecorationDexterity);
    }

    private void Update()
    {
        if (buyBoostForHp)
        {
            
            buttonForDecorationHP.enabled = false;
        }

        if (buyBoostForDexterity)
        {

            buttonForDecorationDeterity.enabled = false;
        }
    }

    private void HpSmall()
    {
        if (BuyItem(7))
        {
            player.inventory.hpSmallBottel += 1;
        }
    }

    private bool BuyItem(int totalPrice)
    {
        if (player.inventory.coins >= totalPrice)
        {
            buyItem.Play();
            player.inventory.coins -= totalPrice;
            return true;
        }
        noMoneyAudioSource.Play();
        return false;
    }

    private void HpBig()
    {
        if (BuyItem(25))
            player.inventory.hpBigBottel += 1;
    }

    void DecorationHP()
    {
        if (BuyItem(25))
        {
            player.HP.DecorationBoost(10);
            buyBoostForHp = true;
            player.inventory.decorationFirstBool = true;
        }
    }
    
    void DecorationDexterity()
    {
        if (BuyItem(25))
        {
            player.inventory.dexterity += 5;
            buyBoostForDexterity = true;
            player.inventory.decorationSecondBool = true;
        }
    }
}
