using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsContainer : UI
{
    public delegate void SnapFunc();
    public RectTransform boxGroup;
    [SerializeField] ContentsBox[] boxes;
    Container container;

    public float snapSpeed = 5f;

    public bool startSwipe = false;

    private void Start()
    {
        container = GetComponentInParent<Container>();
    }

    public override bool onClickDown(Vector2 clickPos)
    {
        return false;
    }

    public override bool onClickUp(float dragDis, Vector2 clickPos)
    {
        if (boxGroup.anchoredPosition.y <= 0.2)
        {
            boxGroup.anchoredPosition = Vector2.zero;
            if (startSwipe)
            {
                startSwipe = false;
                container.ZoomOut(true);
            }
        }
        return false;
    }

    public override bool onSwipe(Vector2 swipeStartp, Vector2 swipeEndp)
    {
        if (boxGroup.anchoredPosition.y - (swipeStartp.y - swipeEndp.y) >= 0 &&
            -1 * (boxGroup.anchoredPosition.y - (swipeStartp.y - swipeEndp.y)) >= (boxes[boxes.Length - 1].GetComponent<RectTransform>().anchoredPosition.y + boxes[boxes.Length - 1].GetComponent<RectTransform>().rect.height / 2 - 25))
        {
            boxGroup.anchoredPosition += new Vector2(0, -1f * (swipeStartp.y - swipeEndp.y));
            if (!startSwipe)
            {
                startSwipe = true;
                container.ZoomIn(true);
            }
            return true;
        }
        else if (boxGroup.anchoredPosition.y - (swipeStartp.y - swipeEndp.y) < 0)
        {
            boxGroup.anchoredPosition = Vector2.zero;
            return true;
        }
        else return false;
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
        p_snapFunc();
    }
}
