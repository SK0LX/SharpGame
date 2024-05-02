using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int coins;
    public int stone;
    public Button buttonForStone;
    void Start()
    {
        var btn = buttonForStone.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    
    void Update()
    {
        
    }

    private void TaskOnClick()
    {
        if (coins >= 10)
        {
            stone += 1;
            coins -= 10;
        }
        else
        {
            print("Недостаточно денег!");
        }
    }
}
