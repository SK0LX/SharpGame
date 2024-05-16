using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private Queue<string> sentences;
    private TextMeshProUGUI dialogText;
    private TextMeshProUGUI nameText;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialog dialog, Canvas canvas, TextMeshProUGUI name, TextMeshProUGUI text)
    {
        canvas.enabled = true;
        nameText = name;
        dialogText = text;
        nameText.text = dialog.name;
        sentences.Clear();
        foreach (var sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public bool DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            return true;
        }
        var sentence = sentences.Dequeue();
        print(sentence);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        return false;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (var letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
