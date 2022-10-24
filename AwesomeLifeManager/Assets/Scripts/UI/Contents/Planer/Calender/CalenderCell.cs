using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalenderCell : MonoBehaviour
{
    public Plan insertedPlan;
    public Calender calender;
    public TextMeshProUGUI tmp;
    [SerializeField] GameObject holdMarker;
    [SerializeField] Image planMarker;

    private void Update()
    {
        
    }

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
        if (insertedPlan == null)
        {
            planMarker.enabled = true;
            insertedPlan = p_plan;
            //calender.accumAP += p_plan.costAP;
            //calender.accumFatigue += p_plan.costFatigue;
            return true;
        }
        else
            return false;
    }

    public void DeletePlan()
    {
        planMarker.enabled = false;
        insertedPlan = null;
    }

    public void Init()
    {
        DeletePlan();
        HoldOut();
    }
}
