using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class triggetText : MonoBehaviour
{
    public Dialog dialog;
    public Button next;

    private void Start()
    {
    }

    public void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialogue(dialog);
    }
}
