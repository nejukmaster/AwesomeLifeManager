using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionCool
{
    public int cool;
    public Action action;

    public ActionCool(int cool, Action action)
    {
        this.cool = cool;
        this.action = action;
    }
}

public class TurnManager : Manager
{
    public static TurnManager instance;

    public Turn currentTurn;
    public int currentTurnNum;
    public GameObject MainUI;
    public Calender calender;
    public TurnProcessPopup turnProcessPopup;
    public Dictionary<string,ActionCool> actionCool = new Dictionary<string,ActionCool>();
    [SerializeField] EventPopup eventPopup;
    [SerializeField] WarningPopup warningPopup;
    PlanManager planManager;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        planManager = PlanManager.instance;
        currentTurn = new Turn(0);
        //actionCool.Add("식재료 구매",new ActionCool(-1,new Action((obj, inform) => { ((EventManager)obj).EnableEvent(10); return true; })));
    }

    public void ReadCalender(Calender p_calender)
    {
        List<Plan> t_planList = new List<Plan>();
        for(int i = 0; i < p_calender.cells.Length; i++)
        {
            for(int j = 0; j < p_calender.cells[i].insertedPlan.Length; j ++)
                t_planList.Add(p_calender.cells[i].insertedPlan[j]);
        }
        currentTurn.settedPlan = t_planList;
    }

    public void RunCurrentTurn()
    {
        if(currentTurn.settedPlan.Count == 0){
            warningPopup.SetActive(true, "먼저 일정을 설계한 뒤 진행해주세요!");
            return;
        }
        turnProcessPopup.gameObject.SetActive(true);
        StartCoroutine(currentTurn.RunningCo(turnProcessPopup,eventPopup));
    }

    public override void Init()
    {
        currentTurn = new Turn(0);
        actionCool["식재료 구매"].cool = -1;
    }
}
