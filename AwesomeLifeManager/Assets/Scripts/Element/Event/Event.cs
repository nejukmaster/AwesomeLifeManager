using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Event
{
    public string name;
    public Sprite mainTex;
    public Choice[] choices = new Choice[3];
    public int exp;

    public Event(string name, Choice[] choices, int exp)
    {
        this.name = name;
        this.choices = choices;
        this.exp = exp;
    }
}
