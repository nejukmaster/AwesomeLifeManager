using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalenderCell : MonoBehaviour
{
    public Plan[] insertedPlan = new Plan[7];
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
        int t_index = Utility.GetEmptyIndex<Plan>(insertedPlan);
        if (t_index < 7)
        {
            insertedPlan[t_index] = p_plan;
            SetPlanMarker();
            return true;
        }
        else
            return false;
    }

    public void SetPlanMarker()
    {
        planMarker.GetComponent<RectTransform>().localScale = new Vector3(Utility.GetNullArrayLength<Plan>(insertedPlan) / 7f, 1f, 0f);
    }

    public void DeletePlan(int planNum)
    {
        insertedPlan[planNum%4] = null;
        calender.checkedPlanIndexes[planNum] = false;
        SetPlanMarker();
    }

    public void Init()
    {
        insertedPlan = new Plan[7];
        SetPlanMarker();
        HoldOut();
    }
}
