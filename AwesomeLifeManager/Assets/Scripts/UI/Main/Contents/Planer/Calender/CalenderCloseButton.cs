using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalenderCloseButton : MonoBehaviour
{
    public GameObject closedPopup;

    [SerializeField] Calender calender;
    [SerializeField] GameObject toClosePopup;
    TurnManager theTurnManager;
    // Start is called before the first frame update
    void Awake()
    {
        theTurnManager = TurnManager.instance;
    }

    public void OnClick()
    {
        if (!calender.weekPlanPopup.gameObject.activeInHierarchy)
        {
            TurnManager.instance.ReadCalender(calender);
            toClosePopup.SetActive(false);
            UI.ToggleSubUI(closedPopup, true);
        }
        else
        {
            calender.weekPlanPopup.SetActive(false,-1);
            UI.ToggleSubUI(calender.container.gameObject, true);
        }
    }
}
