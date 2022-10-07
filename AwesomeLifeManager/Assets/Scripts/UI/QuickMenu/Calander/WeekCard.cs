using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeekCard : Scroll
{
    public int weekNum = 0;
    public float currentX;

    TurnManager theTurnManager;
    ObjectPool theObjectPool;

    private void Awake()
    {
        theTurnManager = TurnManager.instance;
        theObjectPool = ObjectPool.instance;
        rejactSameTimeActing = false;
    }

    public void genPlanBox()
    {
        if (theTurnManager.currentTurn.settedPlan.Count > 0)
        {
            for (int i = 0; i < 7; i++)
            {
                int _i = weekNum * 7 + 1 + i;
                if (theTurnManager.currentTurn.settedPlan[i] != null)
                {
                    GameObject t_box = theObjectPool.weekPlanQueue.Dequeue();
                    t_box.SetActive(true);
                    t_box.GetComponentInChildren<TextMeshProUGUI>().text = theTurnManager.currentTurn.settedPlan[i].name;
                    RectTransform t_rect = t_box.GetComponent<RectTransform>();
                    t_rect.anchoredPosition = new Vector2(0, t_rect.rect.height * i);
                }
            }
        }
    }

    public override void onEndSwipe()
    {
        return;
    }

    public override void onStartSwipe()
    {
        return;
    }
}
