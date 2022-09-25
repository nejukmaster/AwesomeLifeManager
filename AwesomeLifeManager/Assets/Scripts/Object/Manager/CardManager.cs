using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action{
    public delegate void ActionDel(MonoBehaviour obj);

    public ActionDel actionDel;

    public Action(ActionDel actionDel)
    {
        this.actionDel = actionDel;
    }
}
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
    public Action actioin;

    public CardInform(string name, CardType type, string description, string illusteName, string resultDescription, Action actioin)
    {
        this.name = name;
        this.type = type;
        this.description = description;
        this.illusteName = illusteName;
        this.resultDescription = resultDescription;
        this.actioin = actioin;
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
