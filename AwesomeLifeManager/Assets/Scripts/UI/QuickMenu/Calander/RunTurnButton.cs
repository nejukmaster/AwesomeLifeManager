using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTurnButton : MonoBehaviour
{
    QuickMenuManager theQuickMenuManager;
    TurnManager theTurnManager;

    private void Start()
    {
        theQuickMenuManager = QuickMenuManager.instance;
        theTurnManager = TurnManager.instance;
    }
    public void OnClick()
    {
        theQuickMenuManager.activatedButton.OnClick();
        theTurnManager.RunCurrentTurn();
    }
}
