using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// review(30.06.2024): Я обычно избегаю абстрактных названий типа Manager/Controller. Эта штука больше похожа на DialogPlayer
public class DialogManager : MonoBehaviour
{
    private Queue<Message> sentences;
    private TextMeshProUGUI dialogText;
    private TextMeshProUGUI nameText;
    private Image image;
    private Coroutine typingCoroutine;
    [SerializeField] private AudioSource text; // review(30.06.2024): Я бы назвал это textSound, а то текущее название как будто подразумевает, что это какая-то строка

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

    // review(30.06.2024): Не очень понятно, что означает bool. Лучше было бы создать enum типа DialogStatus { NotStarted, Executing, Finished }, чтобы извне класса было понятно, что означает результат действия метода
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

        // review(30.06.2024): код бы чуть-чуть отформатировать, чтобы выделить блоки логики
        var message = sentences.Dequeue();
        if (image is null)
        {
            image = message.imagePerson; // review(30.06.2024): Эту строку можно вынести после блока if, тогда он сократится вообще до одного if, при котором будет image.enabled = false
        }
        else
        {
            image.enabled = false;
            image = message.imagePerson;
        }

        // review(30.06.2024): Код по Reset-у отображения диалога как будто стоит выделить в отдельный метод, а то он дублируется здесь и выше
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // review(30.06.2024): Как будто тут еще надо выключить text на всякий случай
        typingCoroutine = StartCoroutine(TypeSentence(message));
        return false;
    }

    IEnumerator TypeSentence(Message sentence)
    {
        text.Play();
        dialogText.text = "";
        foreach (var letter in sentence.sentence)
        {
            nameText.text = sentence.name;
            if (image is not null)
                image.enabled = true;
            dialogText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
        text.Stop();
    }
}
