using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// review(29.06.2024): Разве не TriggerText?
public class TriggetText : MonoBehaviour
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
        // review(29.06.2024): Может, лучше найти DialogManager единожды при Start?
        FindObjectOfType<DialogManager>().StartDialogue(dialog, canvas, name, text);
    }

    private void Next()
    {
        end = FindObjectOfType<DialogManager>().DisplayNextSentence();
    }
}
