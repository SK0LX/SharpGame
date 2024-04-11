using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Entity _basicEntity = new Entity(100, 20);
    private int stone;
    
    public TextMeshProUGUI textStone;
    public TextMeshProUGUI textHP;
    public TextMeshProUGUI textDexterity;
    void Start()
    {
        
    }

    
    void Update()
    {
        textStone.text = "Монет: " + _basicEntity.Dexterity;
        textHP.text = "Воды: " + _basicEntity.Health;
        textDexterity.text = "Еды: " + stone;
    }
}
