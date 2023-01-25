using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//quicke menu-calender창에서 캘린더 UI를 실행할 버튼의 소스코드입니다.
public class DesignPlanBtn : MonoBehaviour
{
    [SerializeField] GameObject toOpenPopup;
    [SerializeField] GameObject currentPopup;
    [SerializeField] PlanContainer weekCards;

    public void OnClick()
    {
        toOpenPopup.SetActive(true);
        UI.ToggleSubUI(currentPopup, false);
        toOpenPopup.GetComponentInChildren<CalenderCloseButton>().closedPopup = currentPopup;
        if (weekCards.weekCards[weekCards.frontCardIndex].fliped)
            weekCards.weekCards[weekCards.frontCardIndex].Unflip(false);
    }
}
