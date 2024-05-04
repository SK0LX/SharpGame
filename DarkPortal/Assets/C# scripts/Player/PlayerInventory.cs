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
    
    public int coins;
    
    
    public Button buttonHpSmall;
    public TextMeshProUGUI textHpSmall;
    public int hpSmallBottel;
    
    
    public Button buttonForHpBig;
    public TextMeshProUGUI textHpBig;
    public int hpBigBottel;    

    public string nameKnife = "Старый меч";
    public int knifeDamage; 

    private void Update()
    {
        
        textHpBig.text = hpBigBottel.ToString();
        textHpSmall.text = hpSmallBottel.ToString();
    }

    void Start()
    {
        buttonHpSmall.GetComponent<Button>().onClick.AddListener(HpSmall);
        
        buttonForHpBig.GetComponent<Button>().onClick.AddListener(HpBig);
        
        
    }
    

    private void HpSmall()
    {
        if (hpSmallBottel > 0)
        {
            hp.SetHealth(2);
            hpSmallBottel--;
        }
    }
    
    private void HpBig()
    {
        if (hpBigBottel > 0)
        {
            hp.SetHealth(7);
            hpBigBottel--;
        }
    }
}
