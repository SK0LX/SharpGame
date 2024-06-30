using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AudioSpeach : MonoBehaviour
{
    private bool speak = true;
    public Player player;
    [SerializeField] private AudioSource knight1;
    [SerializeField] private AudioSource knight2;
    [SerializeField] private AudioSource knight3;
    [SerializeField] private AudioSource knight4;
    [SerializeField] private AudioSource knight5;

    private AudioSource[] knightsSpeak;

    private void Start()
    {
        knightsSpeak = new[] { knight1, knight2, knight3, knight4, knight5 };
    }

    private void Update()
    {
        if (speak && player.speed > 0)
        {
            StartCoroutine(SpeakKnight());
        }
        else
        {
            StopAllCoroutines(); // review(30.06.2024): кмк не очень оптимально на каждый Update останавливать все корутины, не уверен, что вызов этого метода действительно дешевый
        }
    }

    private IEnumerator SpeakKnight()
    {
        speak = false;
        yield return new WaitForSeconds(30); // review(30.06.2024): Почему именно 30 секунд? Может, стоило это выделить в поле? Возможно, время ожидания как-то связано с временем аудио?
        var rnd = new Random();
        knightsSpeak[rnd.Next(knightsSpeak.Length)].Play();
        yield return new WaitForSeconds(30);
        speak = true;
    }
}
