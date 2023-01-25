using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;

public class Action{
    public delegate void ActionDel(MonoBehaviour obj, CardInform inform);

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
    Angel,
    Job
}
[System.Serializable]
public class CardInform
{
    public List<object> dataList = new List<object>();
    public string name;
    public CardType type;
    public string description;
    public string illusteName;
    public string resultDescription;
    public Action action;
    public int cost;

    public CardInform(string name, CardType type, string description, string illusteName, string resultDescription, Action action, int cost)
    {
        this.name = name;
        this.type = type;
        this.description = description;
        this.illusteName = illusteName;
        this.resultDescription = resultDescription;
        this.action = action;
        this.cost = cost;
    }

    public string GetIllustePath()
    {
        if (type == CardType.Action)
            return "Action/" + illusteName;
        else if (type == CardType.Project)
            return "Project/" + illusteName;
        else if (type == CardType.Event)
            return "Event/" + illusteName;
        else if (type == CardType.Angel)
            return "Angel/" + illusteName;
        else if (type == CardType.Angel)
            return "Job/" + illusteName;
        else
            return "Etc/" + illusteName;
    }
}
public class CardManager : Manager
{

    public static CardManager instance;

    public Dictionary<string, CardInform> cardInformDic = new Dictionary<string, CardInform>();
    public List<Action.ActionDel> actionDelList = new List<Action.ActionDel>();
    public SpriteAtlas illustrationAtlas;

    private void Start()
    {
        instance = this;
        Mapping();
    }

    public void Mapping(){
    }

    public override void Init()
    {

    }
}
