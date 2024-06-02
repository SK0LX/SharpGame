using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private Queue<Message> sentences;
    private TextMeshProUGUI dialogText;
    private TextMeshProUGUI nameText;
    private Image image;
    private Coroutine typingCoroutine;
    [SerializeField] private AudioSource text;

    private void Start()
    {
        sentences = new Queue<Message>();
    }

    public void StartDialogue(Dialog dialog, Canvas canvas, TextMeshProUGUI name, TextMeshProUGUI text)
    {
        canvas.enabled = true;
        nameText = name;
        dialogText = text;
        sentences.Clear();
        foreach (var sentence in dialog.message)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public bool DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            text.Stop();
            return true;
        }
        var message = sentences.Dequeue();
        if (image is null)
        {
            image = message.imagePerson;
        }
        else
        {
            image.enabled = false;
            image = message.imagePerson;
        }
        
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeSentence(message));
        return false;
    }

    IEnumerator TypeSentence(Message sentence)
    {
        dialogText.text = "";
        foreach (var letter in sentence.sentence)
        {
            nameText.text = sentence.name;
            if (image is not null)
                image.enabled = true;
            text.Play();
            dialogText.text += letter;
            yield return new WaitForSeconds(0.11f);
            text.Stop();
        }
    }
}
