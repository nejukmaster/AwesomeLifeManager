using System;
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
        Vector2[] _end = new Vector2[2];
        try{
            _end[0] = objs[objs.Length - 1].GetComponent<RectTransform>().anchoredPosition;
            _end[1] = objs[objs.Length - 1].GetComponent<RectTransform>().sizeDelta;
        }
        catch(IndexOutOfRangeException e)
        {
            _end[0] = Vector2.zero;
            _end[1] = Vector2.zero;
        }
        if (objGroup.anchoredPosition.y - (swipeStartp.y - swipeEndp.y) >= 0 &&
            -1 * (objGroup.anchoredPosition.y - (swipeStartp.y - swipeEndp.y)) >= (_end[0].y + _end[1].y / 2 - 25))
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
