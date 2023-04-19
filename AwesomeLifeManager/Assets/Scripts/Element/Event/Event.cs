using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Event
{
    public string name;
    public Sprite mainTex;
    public Choice[] choices = new Choice[3];
    public bool isStatic;
    public Event(string name, Choice[] choices, bool isStatic)
    {
        this.name = name;
        this.choices = choices;
        this.isStatic = isStatic;
    }
}
