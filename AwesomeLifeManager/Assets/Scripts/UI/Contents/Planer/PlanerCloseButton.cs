using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanerCloseButton : MonoBehaviour
{
    [SerializeField] Calender calender;
    [SerializeField] GameObject MainUI;

    public void OnClick()
    {
        TurnManager.instance.ReadCalender(calender);
        MainUI.SetActive(true);
         GetComponentInParent<PlanerUI>().gameObject.SetActive(false);
    }
}
