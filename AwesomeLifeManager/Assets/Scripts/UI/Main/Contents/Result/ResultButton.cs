using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultButton : MonoBehaviour
{
    TurnManager turnManager;
    [SerializeField] GameObject resultUI;

    void Start()
    {
        turnManager = TurnManager.instance;  
    }
    public void Done()
    {
        turnManager.currentTurnNum += 1;
        turnManager.currentTurn = new Turn(turnManager.currentTurnNum);
        resultUI.SetActive(false);
    }
}
