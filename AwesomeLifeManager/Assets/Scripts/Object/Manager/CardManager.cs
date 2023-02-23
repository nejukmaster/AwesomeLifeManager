using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;

public class Action{
    public delegate bool ActionDel(MonoBehaviour obj, CardInform inform);

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

    public CardInform(CardInform p_inform)
    {
        this.dataList = p_inform.dataList;
        this.name = p_inform.name;
        this.type = p_inform.type;
        this.description = p_inform.description;
        this.illusteName = p_inform.illusteName;
        this.resultDescription = p_inform.resultDescription;
        this.action = p_inform.action;
        this.cost = p_inform.cost;
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

    [SerializeField] Calender calender;

    TurnManager theTurnManager;

    private void Start()
    {
        instance = this;
        theTurnManager = TurnManager.instance;
        Mapping();
    }

    public void Mapping(){
        mapping_action();
        cardInformList.Add(new CardInform("재능 찾기",CardType.Action,"랜덤한 \"능력\"타입의 스테이터스 10~15퍼센트 증가(최소증값 1, 최대증가값 10) ","","",actionDelList["Action_01"],5));
        cardInformList.Add(new CardInform("일과 업무",CardType.Action,"[직업 행동 카드]\n[스트레스] 스탯 상승\n일과 업무를 진행합니다. 아래 스탯에 따라 보상이 달라집니다.\n[성실성], [집중력]","","",actionDelList["Action_02"],10));
        cardInformList.Add(new CardInform("야근",CardType.Action,"[직업 행동 카드]\n[스트레스] 스탯 상승, [건강] 스탯 하락\n일과 업무를 진행합니다. 아래 스탯에 따라 보상이 달라집니다.\n[성실성], [집중력]","","",actionDelList["Action_03"],10));
        cardInformList.Add(new CardInform("해외 출장",CardType.Action,"[직업 행동 카드]\n[스트레스] 스탯 하락, [화술] 스탯 상승\n해외 출장을 다녀옵니다.\n아래 스탯에 따라 보상이 달라집니다.\n[화술], [성실성]","","",actionDelList["Action_04"],5));
        cardInformList.Add(new CardInform("퇴사",CardType.Action,"[자유 행동 카드]\n[성실성] 스탯 상승, [계획성] 스탯 상승, [재력] 스탯 상승\n이번 달 진행한 당신의 일정을 정산합니다.\n매 월 마지막 주에만 정산할 수 있습니다.","","",actionDelList["Action_05"],10));
        cardInformList.Add(new CardInform("월말 정산",CardType.Action,"[자유 행동 카드]\n[성실성] 스탯 상승, [계획성] 스탯 상승, [재력] 스탯 상승\n이번 달 진행한 당신의 일정을 정산합니다.\n매 월 마지막 주에만 정산할 수 있습니다.","","",actionDelList["Action_06"],10));
        cardInformList.Add(new CardInform("연말 정산",CardType.Action,"[자유 행동 카드]\n[성실성] 스탯 상승, [계획성] 스탯 상승, [재력] 스탯 상승\n올해 진행한 당신의 일정을 정산합니다.\n매년 마지막 월에만 정산할 수 있습니다.","","",actionDelList["Action_07"],10));
        cardInformList.Add(new CardInform("식재료 구매",CardType.Action,"[자유 행동 카드]\n[재력] 스탯 하락\n당분간 먹을 식재료를 구매합니다. 아래 스탯에 따라 소모 재화가 달라집니다.\n[재력]","","",actionDelList["Action_08"],5));
        cardInformList.Add(new CardInform("건강 검진",CardType.Action,"[자유 행동 카드]\n[건강] 스탯 상승, [재력] 스탯 하락\n종합 건강 상태를 검진받습니다. 100,000원을 소모합니다.","","",actionDelList["Action_09"],5));
        cardInformList.Add(new CardInform("당일치기 여행",CardType.Action,"","","",actionDelList["Action_10"],15));
        cardInformList.Add(new CardInform("국내 여행",CardType.Action,"","","",actionDelList["Action_11"],20));
        cardInformList.Add(new CardInform("해외 여행",CardType.Action,"","","",actionDelList["Action_12"],30));
        //cardInformList.Add(new CardInform("월세 납입",CardType.Action,"","","",actionDelList["Action_13"],5));
        //cardInformList.Add(new CardInform("병원가기",CardType.Action,"","","",actionDelList["Action_14"],5));
        //cardInformList.Add(new CardInform("영어 공부",CardType.Action,"","","",actionDelList["Action_15"],10));
        //cardInformList.Add(new CardInform("시험 공부",CardType.Action,"","","",actionDelList["Action_16"],10));
    }

    private void mapping_action(){
        actionDelList.Add("Action_01",new Action((cell,t_inform)=>{
            ((CalenderCell)cell).InsertPlan(PlanManager.instance.planDic["Action_01"]);
            return true;
        }));
        actionDelList.Add("Action_02",new Action((cell,t_inform)=>{
            ((CalenderCell)cell).InsertPlan(PlanManager.instance.planDic["Action_02"]);
            return true;
        }));
        actionDelList.Add("Action_03",new Action((cell,t_inform)=>{
            ((CalenderCell)cell).InsertPlan(PlanManager.instance.planDic["Action_03"]);
            return true;
        }));
        actionDelList.Add("Action_04",new Action((cell,t_inform)=>{
            ((CalenderCell)cell).InsertPlan(PlanManager.instance.planDic["Action_04"]);
            return true;
        }));
        actionDelList.Add("Action_05",new Action((cell,t_inform)=>{
            ((CalenderCell)cell).InsertPlan(PlanManager.instance.planDic["Action_05"]);
            return true;
        }));
        actionDelList.Add("Action_06",new Action((cell,t_inform)=>{
            int t = 0;
            for (int i = 0; i < calender.cells.Length; i++)
            {
                if ((CalenderCell)cell == calender.cells[i])
                {
                    t = i;
                    break;
                }
            }
            if (t == calender.cells.Length - 1)
            {
                ((CalenderCell)cell).InsertPlan(PlanManager.instance.planDic["Action_06"]);
                return true;
            }
            else return false;
        }));
        actionDelList.Add("Action_07",new Action((cell,t_inform)=>{
            int t = 0;
            for (int i = 0; i < calender.cells.Length; i++)
            {
                if ((CalenderCell)cell == calender.cells[i])
                {
                    t = i;
                    break;
                }
            }
            if (t == calender.cells.Length - 1 && theTurnManager.currentTurn.turnNum % 12 == 11)
            {
                ((CalenderCell)cell).InsertPlan(PlanManager.instance.planDic["Action_07"]);
                return true;
            }
            else return false;
        }));
        actionDelList.Add("Action_08",new Action((cell,t_inform)=>{
            ((CalenderCell)cell).InsertPlan(PlanManager.instance.planDic["Action_08"]);
            return true;
        }));
        actionDelList.Add("Action_09",new Action((cell,t_inform)=>{
            ((CalenderCell)cell).InsertPlan(PlanManager.instance.planDic["Action_09"]);
            return true;
        }));
    }

    public override void Init()
    {

    }
}
