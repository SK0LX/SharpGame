using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class audioSpeach : MonoBehaviour
{
    private bool speak = true;
    public Player player;
    [SerializeField] private AudioSource knight1;
    [SerializeField] private AudioSource knight2;
    [SerializeField] private AudioSource knight3;
    [SerializeField] private AudioSource knight4;
    [SerializeField] private AudioSource knight5;

    // Update is called once per frame
    private void Update()
    {
        if (speak && player.speed >= 1e-6) // review(24.05.2024): Почему не >= 0?
        {
            StartCoroutine(SpeakKnight());
        }
    }

    private IEnumerator SpeakKnight()
    {
        speak = false;
        yield return new WaitForSeconds(30);
        // review(24.05.2024): Можно все knightN положить в массив и сделать так: knights[rnd.Next(knights.Length)].Play();
        var rnd = new Random();
        switch (rnd.Next(0, 5))
        {
            case 0:
                knight1.Play();
                break;
            case 1:
                knight2.Play();
                break;
            case 2:
                knight3.Play();
                break;
            case 3:
                knight4.Play();
                break;
            case 4:
                knight5.Play();
                break;
        }
        yield return new WaitForSeconds(30);
        speak = true;
    }
}
