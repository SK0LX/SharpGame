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
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI nameText;
    public Canvas canvasForDialog;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialog dialog)
    {
        canvasForDialog.enabled = true;
        nameText.text = dialog.name;
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
            yield return null;
        }
    }
}
