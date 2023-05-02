using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
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
    Effect
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
        else if (type == CardType.Effect)
            return "Effect/" + illusteName;
        else
            return "Etc/" + illusteName;
    }
}
public class CardManager : Manager
{

    public static CardManager instance;

    public Dictionary<string,CardInform> cardInformList = new Dictionary<string, CardInform>();
    public Dictionary<string, Action> actionDelList = new Dictionary<string, Action>();
    public SpriteAtlas illustrationAtlas;

    [SerializeField] Calender calender;

    TurnManager theTurnManager;
    FatigueManager theFatigueManager;
    PlanManager thePlanManager;
    BuffManager theBuffManager;

    private void Start()
    {
        instance = this;
        theTurnManager = TurnManager.instance;
        theFatigueManager = FatigueManager.instance;
        thePlanManager = PlanManager.instance;
        theBuffManager = BuffManager.instance;
        Mapping();
    }

    public void Mapping(){
        mapping_action();
        cardInformList["action_01"] = (new CardInform("병원 방문", CardType.Action, "#\"병약함\" 활성화 시 사용 가능\r\n사용 시 체력 스탯 [5] 증가", "", "", actionDelList["action_01"], 30));
        cardInformList["action_02"] = (new CardInform("전문 상담", CardType.Action, "#\"유리 멘탈\" 활성화 시 사용 가능\r\n사용 시 정신력 스탯 [5] 증가", "", "", actionDelList["action_02"], 40));
        cardInformList["action_03"] = (new CardInform("인성 교육", CardType.Action, "#\"비행 청소년\" 활성화 시 사용 가능\r\n사용 시 양심 [5] 증가", "", "", actionDelList["action_03"], 50));
        cardInformList["action_04"] = (new CardInform("신춘문예", CardType.Action, "* 아래 경우에 따라 진행\r\n[인문 능력 스탯 수치 75 이상일 때 일정확률로] \"입상\" 이벤트 발생\r\n[위 확률 외 경우] \"탈락\" 이벤트 발생 가능", "", "", actionDelList["action_04"], 30));
        cardInformList["action_05"] = (new CardInform("과학탐구 대회", CardType.Action, "* 아래 경우에 따라 진행\r\n[탐구 능력 스탯 수치 75 이상일 때 일정확률로] \"입상\" 이벤트 발생\r\n[위 확률 외 경우] \"탈락\" 이벤트 발생 가능", "", "", actionDelList["action_05"], 30));
        cardInformList["action_06"] = (new CardInform("오디션", CardType.Action, "* 아래 경우에 따라 진행\r\n[예술 능력 스탯 수치 75 이상일 때 일정확률로] \"입상\" 이벤트 발생\r\n[위 확률 외 경우] \"탈락\" 이벤트 발생 가능", "", "", actionDelList["action_06"], 30));
        cardInformList["action_07"] = (new CardInform("사생대회", CardType.Action, "* 아래 경우에 따라 진행\r\n[예술 능력 스탯 수치 75 이상일 때 일정확률로] \"입상\" 이벤트 발생\r\n[위 확률 외 경우] \"탈락\" 이벤트 발생 가능", "", "", actionDelList["action_07"], 30));
        cardInformList["action_08"] = (new CardInform("육상 대회", CardType.Action, "* 아래 경우에 따라 진행\r\n[운동 능력 스탯 수치 75 이상일 때 일정확률로] \"입상\" 이벤트 발생\r\n[위 확률 외 경우] \"탈락\" 이벤트 발생 가능", "", "", actionDelList["action_08"], 30));
        cardInformList["action_09"] = (new CardInform("수학경시대회", CardType.Action, "* 아래 경우에 따라 진행\r\n[탐구 능력 스탯 수치 75 이상일 때 일정확률로] \"입상\" 이벤트 발생\r\n[위 확률 외 경우] \"탈락\" 이벤트 발생 가능", "", "", actionDelList["action_09"], 30));
        cardInformList["action_10"] = (new CardInform("체육 대회", CardType.Action, "* 아래 경우에 따라 진행\r\n[탐구 능력 스탯 수치 75 이상일 때 일정확률로] \"입상\" 이벤트 발생\r\n[위 확률 외 경우] \"탈락\" 이벤트 발생 가능", "", "", actionDelList["action_10"], 30));        
        cardInformList["action_11"] = (new CardInform("학원", CardType.Action, "이벤트 \"학원 등록\" 을 통해 획득 가능.\r\n가장 낮은 능력 스탯을 [2] 상승시킨다.", "", "", actionDelList["action_11"], 35));
        cardInformList["action_12"] = (new CardInform("동아리 활동", CardType.Action, "교내 동아리 활동을 진행한다. 진행 시점 가장 높은 능력 스탯을 상승", "", "", actionDelList["action_12"], 30));
        cardInformList["action_13"] = (new CardInform("조깅", CardType.Action, "\"운동\" 스테이터스 수치가 [65] 이상이거나, 이벤트 \"새해 목표\" 를 통해 획득 가능.\r\n진행 횟수 3회 당 \"체력\" 스테이터스 [1] 상승, \"정신력\" 스테이터스 [1] 상승", "", "", actionDelList["action_13"], 40));
        cardInformList["action_14"] = (new CardInform("독서", CardType.Action, "\"인문\" / \"탐구\" 스테이터스 수치가 [65] 이상이거나, 이벤트 \"새해 목표\" 를 통해 획득 가능.\r\n진행 횟수 3회 당 \"인문\" 스테이터스 [3] 상승, \"탐구\" 스테이터스 [3] 상승", "", "", actionDelList["action_14"], 30));
        cardInformList["action_15"] = (new CardInform("동아리 활동", CardType.Action, "교내 동아리 활동을 진행한다. 진행 시점 가장 높은 능력 스탯을 상승", "", "", actionDelList["action_15"], 30));
        cardInformList["action_16"] = (new CardInform("시험 공부", CardType.Action, "이벤트 \"시험 기간\" 을 통해 획득 가능. 4턴 뒤 이벤트 \"중간고사\", \"기말고사\" 발생 이후 소멸\r\n4턴 간 진행 횟수에 따라 결과 달라짐.\r\n이벤트 \"중간고사\", \"기말고사\" 발생 1주 전 진행 횟수가 0일 경우 이벤트 \"벼락치기\" 발생", "", "", actionDelList["action_16"], 50));
        cardInformList["action_17"] = (new CardInform("심부름", CardType.Action, "이벤트 \"엄마의 부탁\" 을 통해 획득 가능.\r\n진행 시 용돈 획득", "", "", actionDelList["action_17"], 20));
        cardInformList["action_18"] = (new CardInform("숙제하기", CardType.Action, "이벤트 \"숙제\" 를 통해 획득 가능.\r\n진행 시 숙제 완료 판정.", "", "", actionDelList["action_18"], 35));
        
        cardInformList["action_20"] = (new CardInform("자습하기", CardType.Action, "학업 일정 직후 배치 시 학업 일정으로 인해 얻는 스테이터스 수치 상승", "", "", actionDelList["action_20"], 25));
        cardInformList["action_21"] = (new CardInform("아르바이트", CardType.Action, "고등학생 부터 사용 가능. 재화 획득, 조직 스테이터스 상승", "", "", actionDelList["action_21"], 35));
    
    }

    private void mapping_action(){
        actionDelList.Add("action_01", new Action((cell, t_inform) =>
        {
            if(theFatigueManager.Fatigue < t_inform.cost || !theBuffManager.buffs["buff_01"].enabled)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_01"]);
                return true;
            }
        }));
        actionDelList.Add("action_02", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost /*|| !theBuffManager.buffs["buff_02"].enabled*/)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_02"]);
                return true;
            }
        }));
        actionDelList.Add("action_03", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost /*|| !theBuffManager.buffs["buff_02"].enabled*/)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_03"]);
                return true;
            }
        }));
        actionDelList.Add("action_04", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost /*|| !theBuffManager.buffs["buff_02"].enabled*/)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_04"]);
                return true;
            }
        }));
        actionDelList.Add("action_05", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_05"]);
                return true;
            }
        }));
        actionDelList.Add("action_06", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_06"]);
                return true;
            }
        }));
        actionDelList.Add("action_07", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_07"]);
                return true;
            }
        }));
        actionDelList.Add("action_08", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_08"]);
                return true;
            }
        }));
        actionDelList.Add("action_09", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_09"]);
                return true;
            }
        }));
        actionDelList.Add("action_10", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_10"]);
                return true;
            }
        }));
        actionDelList.Add("action_11", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_11"]);
                return true;
            }
        }));
        actionDelList.Add("action_12", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_12"]);
                return true;
            }
        }));
        actionDelList.Add("action_13", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_13"]);
                return true;
            }
        }));
        actionDelList.Add("action_14", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_14"]);
                return true;
            }
        }));
        actionDelList.Add("action_15", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_15"]);
                return true;
            }
        }));
        actionDelList.Add("action_16", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_16"]);
                return true;
            }
        }));
        actionDelList.Add("action_17", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_17"]);
                return true;
            }
        }));
        actionDelList.Add("action_18", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_18"]);
                return true;
            }
        }));

        actionDelList.Add("action_20", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost || Utility.GetEmptyIndex<Plan>(((CalenderCell)cell).insertedPlan) < 1 || ((CalenderCell)cell).insertedPlan[Utility.GetEmptyIndex<Plan>(((CalenderCell)cell).insertedPlan)-1].name != thePlanManager.planDic["study"].name)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_20"]);
                return true;
            }
        }));
        actionDelList.Add("action_20", new Action((cell, t_inform) =>
        {
            if (theFatigueManager.Fatigue < t_inform.cost)
            {
                return false;
            }
            else
            {
                ((CalenderCell)cell).InsertPlan(thePlanManager.planDic["action_20"]);
                return true;
            }
        }));
    }

    public override void Init()
    {

    }
}
