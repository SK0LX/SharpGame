using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class triggetText : MonoBehaviour
{
    public Dialog dialog;
    public Button next;
    public bool end;

    private void Start()
    {
        next.onClick.AddListener(Next);
    }

    public void TriggerDialog(Canvas canvas, TextMeshProUGUI name, TextMeshProUGUI text)
    {
        FindObjectOfType<DialogManager>().StartDialogue(dialog, canvas, name, text);
    }

    private void Next()
    {
        end = FindObjectOfType<DialogManager>().DisplayNextSentence();
    }
}
