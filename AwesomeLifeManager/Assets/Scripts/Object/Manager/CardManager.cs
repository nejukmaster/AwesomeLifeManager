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

    public List<CardInform> cardInformList = new List<CardInform>();
    public Dictionary<string, Action> actionDelList = new Dictionary<string, Action>();
    public SpriteAtlas illustrationAtlas;

    private void Start()
    {
        instance = this;
        Mapping();
    }

    public void Mapping(){
        mapping_action();
        cardInformList.Add(new CardInform("재능 찾기",CardType.Action,"랜덤한 \"능력\"타입의 스테이터스 10~15퍼센트 증가(최소증값 1, 최대증가값 10) ","","",actionDelList["Action_01"],5));
        cardInformList.Add(new CardInform("일과 업무",CardType.Action,"[직업 행동 카드]\n[스트레스] 스탯 상승\n일과 업무를 진행합니다. 아래 스탯에 따라 보상이 달라집니다.\n[성실성], [집중력]","","",actionDelList["Action_02"],10));
        cardInformList.Add(new CardInform("야근",CardType.Action,"","","",actionDelList["Action_03"],10));
        cardInformList.Add(new CardInform("퇴사",CardType.Action,"","","",actionDelList["Action_04"],5));
        cardInformList.Add(new CardInform("월말 정산",CardType.Action,"","","",actionDelList["Action_05"],10));
        //cardInformList.Add(new CardInform("연말 정산",CardType.Action,"","","",actionDelList["Action_06"],10));
        //cardInformList.Add(new CardInform("식재료 구매",CardType.Action,"","","",actionDelList["Action_07"],5));
        //cardInformList.Add(new CardInform("건강 검진",CardType.Action,"","","",actionDelList["Action_08"],5));
        //cardInformList.Add(new CardInform("당일치기 여행",CardType.Action,"","","",actionDelList["Action_09"],15));
        //cardInformList.Add(new CardInform("국내 여행",CardType.Action,"","","",actionDelList["Action_10"],20));
        //cardInformList.Add(new CardInform("해외 여행",CardType.Action,"","","",actionDelList["Action_11"],30));
        //cardInformList.Add(new CardInform("월세 납입",CardType.Action,"","","",actionDelList["Action_12"],5));
        //cardInformList.Add(new CardInform("병원가기",CardType.Action,"","","",actionDelList["Action_13"],5));
        //cardInformList.Add(new CardInform("영어 공부",CardType.Action,"","","",actionDelList["Action_14"],10));
        //cardInformList.Add(new CardInform("시험 공부",CardType.Action,"","","",actionDelList["Action_15"],10));
    }

    private void mapping_action(){
        actionDelList.Add("Action_01",new Action((cell,t_inform)=>{
            ((CalenderCell)cell).InsertPlan(PlanManager.instance.planDic["Action_01"]);
        }));
        actionDelList.Add("Action_02",new Action((cell,t_inform)=>{
            ((CalenderCell)cell).InsertPlan(PlanManager.instance.planDic["Action_02"]);
        }));
        actionDelList.Add("Action_03",new Action((cell,t_inform)=>{
            ((CalenderCell)cell).InsertPlan(PlanManager.instance.planDic["Action_03"]);
        }));
        actionDelList.Add("Action_04",new Action((cell,t_inform)=>{
            ((CalenderCell)cell).InsertPlan(PlanManager.instance.planDic["Action_04"]);
        }));
        actionDelList.Add("Action_05",new Action((cell,t_inform)=>{
            ((CalenderCell)cell).InsertPlan(PlanManager.instance.planDic["Action_05"]);
        }));
    }

    public override void Init()
    {

    }
}
