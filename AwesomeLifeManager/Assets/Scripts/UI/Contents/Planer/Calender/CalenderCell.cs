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

    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] GameObject holdMarker;
    [SerializeField] Image planMarker;
    Calender calender;
    // Start is called before the first frame update

    public void Awake()
    {
        calender = GetComponentInParent<Calender>();
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
        planMarker.enabled = true;
        insertedPlan = p_plan;
        calender.accumAP += p_plan.costAP;
        calender.accumFatigue += p_plan.costFatigue;
        calender.energyPreview.SetPreview(calender.accumFatigue, calender.accumAP);
        return true;
    }
}
