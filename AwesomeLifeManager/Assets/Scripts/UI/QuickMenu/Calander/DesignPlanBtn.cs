using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//???? ?? ?? ??
public class DesignPlanBtn : MonoBehaviour
{
    [SerializeField] GameObject toOpenPopup;
    [SerializeField] GameObject currentPopup;

    public void OnClick()
    {
        toOpenPopup.SetActive(true);
        toOpenPopup.GetComponentInChildren<Calender>().SettingCells();
        UI.ToggleSubUI(currentPopup, false);
        toOpenPopup.GetComponentInChildren<CalenderCloseButton>().closedPoppup = currentPopup;
    }
}
