using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialog
{
    public Message[] message;
}

[System.Serializable]
public class Message
{
    public string name;
    
    [TextArea(3, 10)]
    public string sentence;

    public Image imagePerson;
}
