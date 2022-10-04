using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignPlanBtn : MonoBehaviour
{
    [SerializeField] GameObject toOpenPopup;

    public void OnClick()
    {
        toOpenPopup.SetActive(true);
        toOpenPopup.GetComponentInChildren<Calender>().SettingCells();
    }
}
