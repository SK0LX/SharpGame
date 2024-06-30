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
        // review(30.06.2024): Возможно, не стоит на каждый Update устанавливать fillAmount - это недешевый вызов. Стоит изменять его, когда он действительно поменялся
        // review(30.06.2024): Ладно, глянул реализацию, там оптимизировали этот момент, но тем не менее я бы все равно рекомендовал изменять View через систему событий
        barForDefault.fillAmount = fill;
    }
}
