using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanerPopup : MonoBehaviour
{
    [SerializeField] Hand hand;
    public void SetActive(bool p_bool)
    {
        if (p_bool)
        {
            gameObject.SetActive(p_bool);
            hand.SettingHand(p_bool);
        }
        else
        {
            hand.SettingHand(p_bool);
            gameObject.SetActive(p_bool);
        }
    }
}
