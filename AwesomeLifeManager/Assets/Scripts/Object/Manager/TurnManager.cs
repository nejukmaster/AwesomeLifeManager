using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;

    public Turn currentTurn;
    public List<Plan> settedPlan = new List<Plan>();
    [SerializeField] GameObject planWindow;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentTurn = new Turn(1, planWindow);
    }

    public void RunCurrentTurn()
    {
        StartCoroutine(currentTurn.RunningCo());
    }
}
