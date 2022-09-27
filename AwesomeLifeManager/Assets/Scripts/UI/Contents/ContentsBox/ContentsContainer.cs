using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsContainer : UI
{
    public delegate void SnapFunc();
    [SerializeField] ContentsBox[] boxes;
    [SerializeField] GameObject boxGroup;

    public float snapSpeed = 5f;

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
        boxGroup.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, -1 * (swipeStartp.y - swipeEndp.y));
        return true;
    }

    public IEnumerator SnapCo(RectTransform p_box, SnapFunc p_snapFunc)
    {
        RectTransform t_rect = boxGroup.GetComponent<RectTransform>();
        Vector2 t_dest = new Vector2(0, -1 * p_box.anchoredPosition.y);
        while (Vector2.Distance(t_rect.anchoredPosition,t_dest) >= 0.1)
        {
            t_rect.anchoredPosition = Vector2.Lerp(t_rect.anchoredPosition,
                                                    t_dest,
                                                    snapSpeed * Time.deltaTime);
            yield return null;
        }
        t_rect.anchoredPosition = t_dest;
        p_snapFunc();
    }
}
