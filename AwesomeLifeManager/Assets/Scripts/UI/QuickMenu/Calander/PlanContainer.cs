using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlanContainer : UI
{
    [SerializeField] Vector2 weekCardPosRange = new Vector2(-150, 150);
    [SerializeField] Vector2 weekCardScaleRange = new Vector2(0.7f, 1f);
    [SerializeField] GameObject dropShadow;
    WeekCard[] weekCards = new WeekCard[0];
    int frontCardIndex;
    

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
            Snap();
        }
        return false;
    }

    public override bool onSwipe(Vector2 swipeStartp, Vector2 swipeEndp)
    {
        if (dropShadow.activeSelf)
        {
            dropShadow.SetActive(false);
        }
        float dis = swipeEndp.x - swipeStartp.x;
        for(int i = 0; i < weekCards.Length; i ++)
        {
            RectTransform t_rect = weekCards[i].GetComponent<RectTransform>();
            float t_pos = Utility.pathFunction(weekCards[i].currentX, dis, weekCardPosRange, 2f, 0);
            t_rect.anchoredPosition = new Vector2(t_pos,t_rect.anchoredPosition.y);
            weekCards[i].currentX += dis;
            float t_r = Utility.pathFunction(weekCards[i].currentX, dis, new Vector2(-1, 1), 1f / 75, 1);
            t_r = Utility.Mapping(t_r, new Vector2(-1, 1), weekCardScaleRange);
            float t_scale = t_r;
            t_rect.localScale = new Vector2(t_scale, t_scale);
        }
        return true;
    }

    void Snap()
    {
        for (int i = 0; i < weekCards.Length; i++)
        {
            float _f = weekCards[i].currentX - (weekCards[i].currentX % 75);
            weekCards[i].currentX = _f;
            RectTransform t_rect = weekCards[i].GetComponent<RectTransform>();
            float t_pos = Utility.pathFunction(weekCards[i].currentX, 0, weekCardPosRange, 2f, 0);
            t_rect.anchoredPosition = new Vector2(t_pos, t_rect.anchoredPosition.y);
            float t_r = Utility.pathFunction(weekCards[i].currentX, 0, new Vector2(-1, 1), 1f / 75, 1);
            t_r = Utility.Mapping(t_r, new Vector2(-1, 1), weekCardScaleRange);
            float t_scale = t_r;
            t_rect.localScale = new Vector2(t_scale, t_scale);
        }
        dropShadow.SetActive(true);
        dropShadow.transform.SetAsLastSibling();
        weekCards[GetFrontCard()].transform.SetAsLastSibling();
        frontCardIndex = GetFrontCard();
    }

    int GetFrontCard()
    {
        int r = -1;
        for(int i = 0; i < weekCards.Length; i++)
        {
            if (r == -1) r = i;
            if (weekCards[r].GetComponent<RectTransform>().localScale.x < weekCards[i].GetComponent<RectTransform>().localScale.x) r = i;
        }
        return r;
    }
}
