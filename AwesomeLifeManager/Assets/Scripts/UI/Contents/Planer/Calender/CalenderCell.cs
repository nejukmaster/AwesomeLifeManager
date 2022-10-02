using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalenderCell : MonoBehaviour
{
    public static float width = 122f;
    public static float height = 300f;
    public Plan insertedPlan;
    public Calender calender;
    public TextMeshProUGUI tmp;
    [SerializeField] GameObject holdMarker;
    [SerializeField] Image planMarker;
    
    public void HoldeOn()
    {
        holdMarker.SetActive(true);
    }

    public void HoldOut()
    {
        holdMarker.SetActive(false);
    }

    public bool InsertPlan(Plan p_plan)
    {
        planMarker.enabled = true;
        insertedPlan = p_plan;
        calender.accumAP += p_plan.costAP;
        calender.accumFatigue += p_plan.costFatigue;
        return true;
    }
    
    public void OnClick()
    {
        calender.cellInspectorPopup.SetActive(true, this);
    }
}
