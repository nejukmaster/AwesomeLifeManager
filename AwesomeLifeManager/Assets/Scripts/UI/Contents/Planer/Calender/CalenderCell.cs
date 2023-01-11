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

    ObjectPool theObjectPool;

    private void Awake()
    {
        theObjectPool = FindObjectOfType<ObjectPool>();
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
        DelPlanMarker();
        GenPlanMarker();
    }

    private void GenPlanMarker()
    {
        int pibot = 0;
        for(int i = 0; i < insertedPlan.Length; i++)
        {
            if (insertedPlan[i] != null)
            {
                PlanIcon t_icon = theObjectPool.planIconQueue.Dequeue().GetComponent<PlanIcon>();
                Debug.Log(t_icon);
                t_icon.transform.SetParent(this.transform);
                CardType t_type = CardType.Action;
                if (!insertedPlan[i].canDelete)
                    t_type = CardType.Job;
                t_icon.SetAcitve(true, t_type);
                t_icon.SetPibot(pibot);
                pibot++;
            }
        }
    }

    private void DelPlanMarker()
    {
        foreach(PlanIcon icon in GetComponentsInChildren<PlanIcon>())
        {
            theObjectPool.planIconQueue.Enqueue(icon.gameObject);
            icon.gameObject.SetActive(false);
        }
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
