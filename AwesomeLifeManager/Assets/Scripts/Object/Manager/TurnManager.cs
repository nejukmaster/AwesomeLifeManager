using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;

    public Turn currentTurn;
    public int currentTurnNum;
    [SerializeField] public GameObject planWindow;
    [SerializeField] public GameObject resultUI;

    PlanManager planManager;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        planManager = PlanManager.instance;
        currentTurn = new Turn(1);
    }

    public void ReadCalender(Calender p_calender)
    {
        List<Plan> t_planList = new List<Plan>();
        for(int i = 0; i < p_calender.cells.Length; i++)
        {
            if (p_calender.cells[i].insertedPlan != null)
                t_planList.Add(p_calender.cells[i].insertedPlan);
            else
                t_planList.Add(null);
        }
        currentTurn.settedPlan = t_planList;
    }

    public void RunCurrentTurn()
    {
        StartCoroutine(currentTurn.RunningCo());
    }
}
