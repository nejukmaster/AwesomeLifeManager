using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlanContainer : UI
{
    [SerializeField] Vector2 weekCardPosRange = new Vector2(-150, 150);
    [SerializeField] Vector2 weekCardScaleRange = new Vector2(0.7f, 1f);
    [SerializeField] GameObject weekView;
    public GameObject dropShadow;
    WeekCard[] weekCards = new WeekCard[0];
    int frontCardIndex = 0;
    TurnManager theTurnManager;
    

    private void Awake()
    {
        weekCards = GetComponentsInChildren<WeekCard>();
        theTurnManager = TurnManager.instance;
        frontCardIndex = GetFrontCard();
        weekView.GetComponentInChildren<TextMeshProUGUI>().text = (theTurnManager.currentTurn.turnNum % 12 +1).ToString()+"월 "+(weekCards[frontCardIndex].weekNum+1).ToString()+"주차";
    }
    public override bool onClickDown(Vector2 clickPos)
    {
        return false;
    }

    public override bool onClickUp(float dragDis, Vector2 clickPos)
    {
        Snap();
        if(dragDis < 20)
        {
           weekCards[frontCardIndex].OnClick();
            return true;
        }
        return false;
    }

    public override bool onSwipe(Vector2 swipeStartp, Vector2 swipeEndp)
    {
        if (dropShadow.activeSelf)
        {
            weekCards[frontCardIndex].objGroup.gameObject.SetActive(false);
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
            if(GetFrontCard() != frontCardIndex)
            {
                frontCardIndex = GetFrontCard();
                weekCards[frontCardIndex].transform.SetAsLastSibling();
                weekView.GetComponentInChildren<TextMeshProUGUI>().text = (theTurnManager.currentTurn.turnNum % 12 +1).ToString()+"월 "+(weekCards[frontCardIndex].weekNum+1).ToString()+"주차";
            }
        }
        return true;
    }

    void Snap()
    {
        float f = weekCards[0].currentX % 75;
        for (int i = 0; i < weekCards.Length; i++)
        {
            weekCards[i].currentX -= f;
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
        weekCards[frontCardIndex].objGroup.gameObject.SetActive(true);
        weekView.GetComponentInChildren<TextMeshProUGUI>().text = (theTurnManager.currentTurn.turnNum % 12 +1).ToString()+"월 "+(weekCards[frontCardIndex].weekNum+1).ToString()+"주차";
        weekCards[frontCardIndex].GetComponent<RectTransform>().localScale = Vector2.one;
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
