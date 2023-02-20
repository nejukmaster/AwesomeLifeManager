using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice
{
    public delegate bool EventDel(MonoBehaviour obj, EventItem item);

    public string name;
    public string text;
    public Sprite texture;

    public Choice(string name, string text)
    {
        this.name = name;
        this.text = text;
    }
}
