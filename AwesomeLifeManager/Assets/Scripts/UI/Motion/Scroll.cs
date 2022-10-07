using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class Scroll : UI
{

    public RectTransform objGroup;
    public RectTransform[] objs;
    public bool startSwipe = false;
    public bool rejactSameTimeActing = true;

    private void Awake()
    {
        objs = objGroup.GetComponentsInChildren<RectTransform>();
    }

    public override bool onClickDown(Vector2 clickPos)
    {
        return false;
    }

    public override bool onClickUp(float dragDis, Vector2 clickPos)
    {
        if (objGroup.anchoredPosition.y <= 0.2)
        {
            objGroup.anchoredPosition = Vector2.zero;
            if (startSwipe)
            {
                startSwipe = false;
                onEndSwipe();
            }
        }
        return false;
    }

    public override bool onSwipe(Vector2 swipeStartp, Vector2 swipeEndp)
    {
        if (objGroup.anchoredPosition.y - (swipeStartp.y - swipeEndp.y) >= 0 &&
            -1 * (objGroup.anchoredPosition.y - (swipeStartp.y - swipeEndp.y)) >= (objs[objs.Length - 1].GetComponent<RectTransform>().anchoredPosition.y + objs[objs.Length - 1].GetComponent<RectTransform>().rect.height / 2 - 25))
        {
            objGroup.anchoredPosition += new Vector2(0, -1f * (swipeStartp.y - swipeEndp.y));
            if (!startSwipe)
            {
                startSwipe = true;
                onStartSwipe();
            }
            return rejactSameTimeActing;
        }
        else if (objGroup.anchoredPosition.y - (swipeStartp.y - swipeEndp.y) < 0)
        {
            objGroup.anchoredPosition = Vector2.zero;
            return rejactSameTimeActing;
        }
        else return false;
    }

    public abstract void onStartSwipe();

    public abstract void onEndSwipe();
}
