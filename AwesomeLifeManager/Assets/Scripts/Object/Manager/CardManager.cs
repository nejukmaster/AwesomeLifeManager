using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Action,
    Project,
    Event,
    Angel
}
[System.Serializable]
public class CardInform
{
    public string name;
    public CardType type;
    public string description;
    public string illusteName;
    public string resultDescription;

    public CardInform(string name, CardType type, string description, string illusteName, string resultDescription)
    {
        this.name = name;
        this.type = type;
        this.description = description;
        this.illusteName = illusteName;
        this.resultDescription = resultDescription;
    }

    public string GetIllustePath()
    {
        if (type == CardType.Action)
            return "Action/" + illusteName + ".png";
        else if (type == CardType.Project)
            return "Project/" + illusteName + ".png";
        else if (type == CardType.Event)
            return "Event/" + illusteName + ".png";
        else if (type == CardType.Angel)
            return "Angel/" + illusteName + ".png";
        else
            return "Etc/" + illusteName + ".png";
    }
}
public class CardManager : MonoBehaviour
{

}
