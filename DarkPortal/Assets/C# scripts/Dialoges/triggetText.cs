using System.Collections;
using System.Collections.Generic;
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

    public void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialogue(dialog);
    }

    private void Next()
    {
        end = FindObjectOfType<DialogManager>().DisplayNextSentence();
    }
}
