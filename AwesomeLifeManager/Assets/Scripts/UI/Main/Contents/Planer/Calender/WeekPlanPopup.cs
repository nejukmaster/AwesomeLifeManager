using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeekPlanPopup : Scroll
{
    public int weekNum;

    [SerializeField] GameObject date;
    [SerializeField] GameObject paymentViewer;
    [SerializeField] GameObject planContianer;
    [SerializeField] Calender calender;
    TurnManager theTurnManager;
    ObjectPool theObjectPool;
    public void SetActive(bool p_bool, int p_weekNum)
    {
        if (theTurnManager == null)
            theTurnManager = TurnManager.instance;
        if (theObjectPool == null)
            theObjectPool = ObjectPool.instance;
        if (p_bool)
        {
            weekNum = p_weekNum;
            this.gameObject.SetActive(true);
            date.GetComponentInChildren<TextMeshProUGUI>().text = (theTurnManager.currentTurn.turnNum % 12 + 1).ToString() + "월 " + (p_weekNum + 1).ToString() + "주차";
            genPlanBox(p_weekNum, this.objGroup);
        }
        else
        {
            declarePlanBox();
            this.gameObject.SetActive(false);
        }
    }

    public void genPlanBox(int p_weekNum, RectTransform p_parent)
    {
        int pibot = 0;
        if (calender.cells[p_weekNum].insertedPlan != null)
        {
            for (int j = 0; j < calender.cells[p_weekNum].insertedPlan.Length; j ++) {
                if (calender.cells[p_weekNum].insertedPlan[j] != null)
                {
                    GameObject t_box = theObjectPool.weekPlanQueue.Dequeue();
                    t_box.SetActive(true);
                    t_box.transform.SetParent(p_parent.transform, false);
                    t_box.GetComponentInChildren<TextMeshProUGUI>().text = calender.cells[p_weekNum].insertedPlan[j].name;
                    RectTransform t_rect = t_box.GetComponent<RectTransform>();
                    t_rect.localScale = Vector2.one;
                    t_rect.anchoredPosition = new Vector2(0, -1 * t_rect.rect.height * pibot);
                    t_box.GetComponent<PlanBox>().planNum = p_weekNum * 4 + j;

                    t_box.GetComponentInChildren<Toggle>().gameObject.SetActive(true);
                    if (calender.checkedPlanIndexes[p_weekNum * 4 + j])
                        t_box.GetComponentInChildren<Toggle>().isOn = true;
                    else
                        t_box.GetComponentInChildren<Toggle>().isOn = false;
                    pibot++;
                }
            }
        }
        updateObjs<PlanBox>();
    }

    public void declarePlanBox()
    {
        for (int i = 0; i < objs.Length; i++)
        {
            theObjectPool.weekPlanQueue.Enqueue(objs[i].gameObject);
            objs[i].gameObject.SetActive(false);
        }
        updateObjs<PlanBox>();
    }



    public override void onStartSwipe()
    {
        return;
    }

    public override void onEndSwipe()
    {
        return;
    }
}