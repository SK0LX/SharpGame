using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public Player player;
    private int coinsPLayer; // review(24.05.2024): Переменная как будто не используется

    // review(24.05.2024): Поля точно должны быть публичными?
    public Button buttonForSmallHP;
    public Button buttonForBigHP;
    public Button buttonForDecorationHP;
    public bool buy1HP;
    public Button buttonForDecorationDeterity;
    public bool buy2Deterity;
    [SerializeField] private AudioSource noMonie; // review(24.05.2024): noMoneyAudioSource
    void Start()
    {
        // review(24.05.2024): Вы берете компоненту кнопки, потому что в кнопке кнопка?
        buttonForSmallHP.GetComponent<Button>().onClick.AddListener(HpSmall);
        
        buttonForBigHP.GetComponent<Button>().onClick.AddListener(HpBig);
        
        buttonForDecorationHP.GetComponent<Button>().onClick.AddListener(DecorationHP);
        
        buttonForDecorationDeterity.GetComponent<Button>().onClick.AddListener(DecorationDexterity);
    }

    private void Update()
    {
        // review(24.05.2024): не совсем понятна логика. Это нужно, чтобы игрок не мог купить много HP?
        if (buy1HP)
        {
            
            buttonForDecorationHP.enabled = false;
        }

        if (buy2Deterity)
        {

            buttonForDecorationDeterity.enabled = false;
        }
    }

    // review(24.05.2024): Повторяется логика
    // 1. Если у игрока не хватает денег, просто проиграй звук
    // 2. Иначе уменьши количество денег игрока и сделай что-то
    // Предлагаю все, кроме "сделай что-то" вынести в общий код. Например, можно использовать структуру
    // public record ShopItem(int Price, Action OnBuyAction);
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
