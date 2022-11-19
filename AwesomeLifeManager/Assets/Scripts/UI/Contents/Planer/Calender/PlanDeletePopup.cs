using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanDeletePopup : MonoBehaviour
{
    [SerializeField] Calender calender;
    [SerializeField] TextMeshProUGUI tmp;

    ObjectPool theObjectPool;

    public void Awake()
    {
        theObjectPool = ObjectPool.instance;
    }

    public void SetActive(bool p_bool, string? p_str)
    {
        this.gameObject.SetActive(p_bool);
        if (p_bool && p_str != null)
        {
            tmp.text = p_str;
        }
    }
    public void Yes()
    {
        if (calender.checkedPlanIndexes.Count > 0)
        {
            foreach (int i in calender.checkedPlanIndexes)
            {
                calender.cells[i].DeletePlan();
            }
            if (calender.weekPlanPopup.gameObject.activeInHierarchy)
            {
                if (calender.weekPlanPopup.objs.Length > 0)
                {
                    for (int i = 0; i < calender.weekPlanPopup.objs.Length; i++)
                    {
                        GameObject obj = calender.weekPlanPopup.objs[i].gameObject;
                        if (obj.GetComponentInChildren<Toggle>().isOn)
                        {
                            obj.GetComponentInChildren<Toggle>().isOn = false;
                            obj.SetActive(false);
                            for (int j = i + 1; j < calender.weekPlanPopup.objs.Length; j++)
                            {
                                RectTransform t_rect = calender.weekPlanPopup.objs[j].GetComponent<RectTransform>();
                                t_rect.anchoredPosition += new Vector2(0, calender.weekPlanPopup.objs[i].rect.height);
                            }
                            theObjectPool.weekPlanQueue.Enqueue(obj.gameObject);
                        }
                    }
                }
            }
            calender.fatiguePreview.Setting();
        }
        this.SetActive(false, null);
    }
    public void No()
    {
        this.SetActive(false, null);
    }
}
