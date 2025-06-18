using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogLine
{
    public string text;
    public bool showNameTag;

    public DialogLine(string text, bool showNameTag = true)
    {
        this.text = text;
        this.showNameTag = showNameTag;
    }
}
