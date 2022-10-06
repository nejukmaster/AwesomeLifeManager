using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlanContainer : UI
{
    [SerializeField] Vector2 weekCardPosRange = new Vector2(-150, 150);
    [SerializeField] Vector2 weekCardScaleRange = new Vector2(0.7f, 1f);
    WeekCard[] weekCards = new WeekCard[0];

    private void Awake()
    {
        weekCards = GetComponentsInChildren<WeekCard>();
    }
    public override bool onClickDown(Vector2 clickPos)
    {
        return false;
    }

    public override bool onClickUp(float dragDis, Vector2 clickPos)
    {
        if(dragDis > 0)
        {
            StartCoroutine(SnapCo());
        }
        return false;
    }

    public override bool onSwipe(Vector2 swipeStartp, Vector2 swipeEndp)
    {
        float dis = swipeEndp.x - swipeStartp.x;
        for(int i = 0; i < weekCards.Length; i ++)
        {
            RectTransform t_rect = weekCards[i].GetComponent<RectTransform>();
            float t_pos = Utility.pathFunction(weekCards[i].currentX, dis, weekCardPosRange, 2.5f, 0);
            t_rect.anchoredPosition = new Vector2(t_pos,t_rect.anchoredPosition.y);
            weekCards[i].currentX += dis;
            float t_scale = Utility.pathFunction(weekCards[i].currentX, dis +1, weekCardScaleRange,1f/300f, 1);
            t_rect.localScale = new Vector2(t_scale, t_scale);
        }
        return true;
    }

    IEnumerator SnapCo()
    {
        float f = ((int)(weekCards[0].currentX / 150) + 1) * 150f;
        while (weekCards[0].currentX < f)
        {
            for(int i = 0; i < weekCards.Length; i++)
            {
                RectTransform t_rect = weekCards[i].GetComponent<RectTransform>();
                float t_pos = Utility.pathFunction(weekCards[i].currentX, 10, weekCardPosRange, 2.5f, 0);
                t_rect.anchoredPosition = new Vector2(t_pos, t_rect.anchoredPosition.y);
                weekCards[i].currentX += 10;
                float t_scale = Utility.pathFunction(weekCards[i].currentX, 10 + 1, weekCardScaleRange, 1f / 300f, 1);
                t_rect.localScale = new Vector2(t_scale, t_scale);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        for (int i = 0; i < weekCards.Length; i++)
        {
            RectTransform t_rect = weekCards[i].GetComponent<RectTransform>();
            weekCards[i].currentX = weekCards[i].currentX + (f - weekCards[0].currentX);
            float t_pos = Utility.pathFunction(weekCards[i].currentX, 0, weekCardPosRange, 2.5f, 0);
            t_rect.anchoredPosition = new Vector2(t_pos, t_rect.anchoredPosition.y);
            float t_scale = Utility.pathFunction(weekCards[i].currentX, 1, weekCardScaleRange, 1f / 300f, 1);
            t_rect.localScale = new Vector2(t_scale, t_scale);
        }
    }
}
