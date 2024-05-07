using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image barForDefault;
    public float fill;
    void Start()
    {
        fill = 1f;
    }

    
    void Update()
    {
        barForDefault.fillAmount = fill;
    }
}
