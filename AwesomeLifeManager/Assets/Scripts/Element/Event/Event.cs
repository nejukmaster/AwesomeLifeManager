using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Event
{
    public string name;
    public Texture2D mainTex;
    public Choice[] choices = new Choice[3];

    public Event(string name, Choice[] choices)
    {
        this.name = name;
        this.choices = choices;
    }
}
