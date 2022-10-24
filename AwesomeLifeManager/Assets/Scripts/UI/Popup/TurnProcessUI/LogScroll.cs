using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LogScroll : Scroll
{
    public override void onEndSwipe()
    {
        return;
    }

    public override void onStartSwipe()
    {
        return;
    }

    public IEnumerator ScrollCo(float t_y)
    {
        RectTransform t_rect = objGroup.GetComponent<RectTransform>();
        float t_destY = t_rect.anchoredPosition.y + t_y;
        while(t_destY - t_rect.anchoredPosition.y >= 1)
        {
            t_rect.anchoredPosition = Vector2.Lerp(t_rect.anchoredPosition,
                                                    new Vector2(0, t_destY),
                                                    10f * Time.deltaTime);
            yield return null; 
        }
        t_rect.anchoredPosition = new Vector2(0, t_destY);
    }
}
