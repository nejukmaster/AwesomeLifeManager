using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanContainer : UI
{
    [SerializeField] Vector2 weekCardPosRange = new Vector2(-150, 150);
    [SerializeField] Vector2 weekCardScaleRange = new Vector2(0.6f, 1f);
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
        return false;
    }

    public override bool onSwipe(Vector2 swipeStartp, Vector2 swipeEndp)
    {
        float dis = swipeEndp.x - swipeStartp.x;
        for(int i = 0; i < weekCards.Length; i ++)
        {
            RectTransform t_rect = weekCards[i].GetComponent<RectTransform>();
            float t_pos = Utility.pathFunction(t_rect.anchoredPosition.x, dis, weekCardPosRange);
            t_rect.anchoredPosition = new Vector2(t_pos,t_rect.anchoredPosition.y);
            float t_scale = Utility.pathFunction(t_rect.localScale.x, dis, weekCardScaleRange);
            t_rect.localScale = new Vector2(t_scale, t_scale);
        }
        return true;
    }
}
