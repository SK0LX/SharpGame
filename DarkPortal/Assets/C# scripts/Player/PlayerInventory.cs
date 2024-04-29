using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int stone;
    
    public TextMeshProUGUI textStone;
    public TextMeshProUGUI textHP;
    public TextMeshProUGUI textDexterity;
    void Start()
    {
        
    }

    
    void Update()
    {
        textDexterity.text = "Еды: " + stone;
    }
}
