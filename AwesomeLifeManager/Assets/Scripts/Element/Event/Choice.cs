using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice
{
    public string name;
    public string text;
    public Sprite texture;
    public string parentEvent;
    public System.Action<MonoBehaviour> reward;

    public Choice(string name, string text, System.Action<MonoBehaviour> reward)
    {
        this.name = name;
        this.text = text;
        this.reward = reward;
    }
}
