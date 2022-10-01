using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignPlanBtn : MonoBehaviour
{
    [SerializeField] GameObject toClosePopup;
    [SerializeField] GameObject toOpenPopup;

    public void OnClick()
    {
        toClosePopup.SetActive(false);
        toOpenPopup.SetActive(true);
    }
}
