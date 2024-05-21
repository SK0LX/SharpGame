using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class showText : MonoBehaviour
{
    [SerializeField] private GameObject heart;

    void Start() 
    {
        heart.SetActive(false);
    }

    private void OnMouseOver() 
    {
        heart.SetActive(true);   //показываем объект с текстом
    }

    private void OnMouseExit() 
    {
        heart.SetActive(false);   //пряем текст (если нужно)
    }
}