using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;

    public Turn currentTurn;
    public int currentTurnNum;
    public List<Plan> settedPlan = new List<Plan>();
    [SerializeField] public GameObject planWindow;
    [SerializeField] public GameObject resultUI;

    PlanManager planManager;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        planManager = PlanManager.instance;
        currentTurn = new Turn(1);
        currentTurn.settedPlan.Add(planManager.planDic["00"]);
        currentTurn.settedPlan.Add(planManager.planDic["01"]);
        currentTurn.settedPlan.Add(planManager.planDic["02"]);
    }

    public void RunCurrentTurn()
    {
        StartCoroutine(currentTurn.RunningCo());
    }
}
