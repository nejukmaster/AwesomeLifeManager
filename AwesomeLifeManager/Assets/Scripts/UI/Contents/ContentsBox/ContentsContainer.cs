using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsContainer : UI
{
    [SerializeField] ContentsBox[] boxes;

    public override bool onClickDown(Vector2 clickPos)
    {
        return false;
    }

    public override bool onClickUp(float dragDis, Vector2 clickPos)
    {
        return false ;
    }

    public override bool onSwipe(Vector2 swipeStartp, Vector2 swipeEndp)
    {
        this.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, -1 * (swipeStartp.y - swipeEndp.y));
        return true;
    }
}
