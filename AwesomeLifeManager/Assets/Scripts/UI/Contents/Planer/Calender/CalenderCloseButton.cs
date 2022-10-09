using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalenderCloseButton : MonoBehaviour
{
    public GameObject closedPoppup;

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
        TurnManager.instance.ReadCalender(calender);
        toClosePopup.SetActive(false);
        UI.ToggleSubUI(closedPoppup, true);
    }
}
