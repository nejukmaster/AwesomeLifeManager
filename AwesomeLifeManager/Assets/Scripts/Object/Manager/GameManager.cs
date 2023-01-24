using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    TurnManager theTurnManager;
    JobManager theJobManager;
    EventManager theEventManager;
    StatusManager theStatusManager;
    PlanManager thePlanManager;
    CardManager theCardManager;

    // Start is called before the first frame update
    void Start()
    {
        theTurnManager = TurnManager.instance;
        theJobManager = JobManager.instance;
        theEventManager = EventManager.instance;
        theStatusManager = StatusManager.instance;
        thePlanManager = PlanManager.instance;
        theCardManager = CardManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitGame()
    {
        theTurnManager.currentTurn = new Turn(0);
    }
}
