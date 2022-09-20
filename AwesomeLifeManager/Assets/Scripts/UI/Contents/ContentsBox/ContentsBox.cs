using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContentsBox : MonoBehaviour
{

    public abstract void OnStartCoroutine();
    public abstract void OnEndCoroutine();

    public IEnumerator SizeCo()
    {
        RectTransform t_uiRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        RectTransform t_rect = GetComponent<RectTransform>();
        Vector2 t_size = t_rect.sizeDelta;
        Vector2 t_pos = t_rect.anchoredPosition;
        OnStartCoroutine();
        while (Vector2.Distance(t_rect.sizeDelta, t_uiRect.sizeDelta + new Vector2(200, 200)) >= 1)
        {
            t_rect.sizeDelta = Vector2.Lerp(t_rect.sizeDelta,
                                            t_uiRect.sizeDelta + new Vector2(200, 200),
                                            6f * Time.deltaTime);
            t_rect.anchoredPosition = Vector2.Lerp(t_rect.anchoredPosition,
                                                    new Vector2(t_rect.anchoredPosition.x, 0),
                                                    6f * Time.deltaTime);
            yield return null;
        }
        t_rect.sizeDelta = t_size;
        t_rect.anchoredPosition = t_pos;
        OnEndCoroutine();
    }
}
