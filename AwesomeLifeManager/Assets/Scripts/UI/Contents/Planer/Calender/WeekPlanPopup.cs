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
        for (int i = 0; i < 7; i++)
        {
            int _i = p_weekNum * 7 + i;
            if (calender.cells[_i].insertedPlan != null)
            {
                GameObject t_box = theObjectPool.weekPlanQueue.Dequeue();
                t_box.SetActive(true);
                t_box.transform.SetParent(p_parent.transform, false);
                t_box.GetComponentInChildren<TextMeshProUGUI>().text = (i + 1) + "일차: " + calender.cells[_i].insertedPlan.name;
                RectTransform t_rect = t_box.GetComponent<RectTransform>();
                t_rect.localScale = Vector2.one;
                t_rect.anchoredPosition = new Vector2(0, -1 * t_rect.rect.height * pibot);
                t_box.GetComponent<PlanBox>().planNum = _i;
                t_box.GetComponentInChildren<Toggle>().gameObject.SetActive(true);
                pibot++;
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
