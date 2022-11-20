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
            if (p_calender.cells[i].insertedPlan != null)
                t_planList.Add(p_calender.cells[i].insertedPlan);
            else
                t_planList.Add(null);
        }
        currentTurn.settedPlan = t_planList;
    }

    public void RunCurrentTurn()
    {
        turnProcessPopup.gameObject.SetActive(true);
        StartCoroutine(currentTurn.RunningCo(turnProcessPopup,eventPopup));
    }
}
