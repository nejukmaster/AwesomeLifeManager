using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsContainer : Scroll
{
    public delegate void SnapFunc();
    //public RectTransform boxGroup;
    [SerializeField] RectTransform[] boxes;
    Container container;

    public float snapSpeed = 5f;

    //public bool startSwipe = false;

    private void Start()
    {
        container = GetComponentInParent<Container>();
        this.objs = boxes;
    }

    public override void onStartSwipe()
    {
        container.ZoomIn(true);
    }

    public override void onEndSwipe()
    {
        container.ZoomOut(true);
    }

    public IEnumerator SnapCo(RectTransform p_box, SnapFunc p_snapFunc)
    {
        RectTransform t_rect = objGroup.GetComponent<RectTransform>();
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
