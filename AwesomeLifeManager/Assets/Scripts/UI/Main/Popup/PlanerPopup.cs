using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanerPopup : MonoBehaviour
{
    [SerializeField] Hand hand;
    [SerializeField] CardInfoPopup cardInfoPopup;
    [SerializeField] Calender calender;
    public void SetActive(bool p_bool)
    {
        if (p_bool)
        {
            gameObject.SetActive(p_bool);
            hand.SettingHand(p_bool);
            if (!calender.cellsSetted)
                calender.InitialCells();
        }
        else
        {
            hand.SettingHand(p_bool);
            cardInfoPopup.SetActive(p_bool, null);
            gameObject.SetActive(p_bool);
        }
    }
}
