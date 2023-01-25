using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTurnButton : MonoBehaviour
{
    QuickMenuManager theQuickMenuManager;
    TurnManager theTurnManager;
    [SerializeField] PlanContainer planContainer;

    private void Start()
    {
        theQuickMenuManager = QuickMenuManager.instance;
        theTurnManager = TurnManager.instance;
    }
    public void OnClick()
    {
        if (planContainer.weekCards[planContainer.frontCardIndex].fliped)
        {
            planContainer.weekCards[planContainer.frontCardIndex].Unflip(false);
        }
        theQuickMenuManager.activatedButton.OnClick();
        theTurnManager.RunCurrentTurn();
    }
}
