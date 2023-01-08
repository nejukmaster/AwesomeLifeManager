using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;

    public Turn currentTurn;
    public int currentTurnNum;
    public GameObject MainUI;
    public Calender calender;
    public TurnProcessPopup turnProcessPopup;
    [SerializeField] EventPopup eventPopup;
    [SerializeField] WarningPopup warningPopup;
    PlanManager planManager;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        planManager = PlanManager.instance;
        currentTurn = new Turn(0);
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
}
