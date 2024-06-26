using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Health hp;
    public int dexterity;
    public int coins;
    
    public Image decorationFirst;
    public bool decorationFirstBool;
    public Image decorationSecond;
    public bool decorationSecondBool;
    
    
    public Button buttonHpSmall;
    public TextMeshProUGUI textHpSmall;
    public int hpSmallBottel;
    
    
    public Button buttonForHpBig;
    public TextMeshProUGUI textHpBig;
    public int hpBigBottel;    
    
    public TextMeshProUGUI textDexterity;
    public TextMeshProUGUI textCoins;
    [SerializeField] private AudioSource drink;

    private void Update()
    {
        textHpBig.text = hpBigBottel.ToString();
        textHpSmall.text = hpSmallBottel.ToString();
        textDexterity.text = $"Ловкость:{dexterity}";
        textCoins.text = coins.ToString();

        if (decorationFirstBool)
        {
            decorationFirst.enabled = true;
        }
        
        if (decorationSecondBool)
        {
            decorationSecond.enabled = true;
        }
    }

    void Start()
    {
        buttonHpSmall.GetComponent<Button>().onClick.AddListener(HpSmall);
        
        buttonForHpBig.GetComponent<Button>().onClick.AddListener(HpBig);

        dexterity = 2;
    }
    

    private void HpSmall()
    {
        if (hpSmallBottel > 0)
        {
            drink.Play();
            hp.SetHealth(3);
            hpSmallBottel--;
        }
    }
    
    private void HpBig()
    {
        if (hpBigBottel > 0)
        {
            drink.Play();
            hp.SetHealth(8);
            hpBigBottel--;
        }
    }
}
