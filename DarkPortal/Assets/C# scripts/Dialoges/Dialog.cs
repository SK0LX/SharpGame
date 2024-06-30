using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialog
{
    public Message[] message;
}

// review(29.06.2024): Стоит вынести в отдельный файл
[System.Serializable]
public class Message
{
    public string name;
    
    [TextArea(3, 10)]
    public string sentence;

    public Image imagePerson;
}
