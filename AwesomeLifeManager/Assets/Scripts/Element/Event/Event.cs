using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Event
{
    public delegate bool ConditionDel();

    public string name;
    public Texture2D mainTex;
    public Choice[] choices = new Choice[3];
    public ConditionDel conditionDel;

    public Event(string name, Choice[] choices)
    {
        this.name = name;
        this.choices = choices;
        conditionDel = () => { return true; };
    }

    public Event(string name, Choice[] choices, ConditionDel del)
    {
        this.name = name;
        this.choices = choices;
        this.conditionDel = del;
    }
}
