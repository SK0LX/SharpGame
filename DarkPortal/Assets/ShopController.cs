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
    
    void Start()
    {
        buttonForSmallHP.GetComponent<Button>().onClick.AddListener(HpSmall);
        
        buttonForBigHP.GetComponent<Button>().onClick.AddListener(HpBig);


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
            print("Недостаточно средств");
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
            print("Недостаточно средств");
        }
    }
}
