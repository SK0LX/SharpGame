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
            StopAllCoroutines();
        }
    }

    private IEnumerator SpeakKnight()
    {
        speak = false;
        yield return new WaitForSeconds(30);
        var rnd = new Random();
        knightsSpeak[rnd.Next(knightsSpeak.Length)].Play();
        yield return new WaitForSeconds(30);
        speak = true;
    }
}
