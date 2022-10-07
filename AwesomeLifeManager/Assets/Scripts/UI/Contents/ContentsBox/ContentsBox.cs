using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ContentsBox : MonoBehaviour
{
    [SerializeField] public GameObject contentsBoxPopup;

    [SerializeField] RectTransform targetSize;
    public abstract void OnStartCoroutine();
    public abstract void OnEndCoroutine();

    public abstract void OnClick();

    public IEnumerator SizeCo()
    {
        RectTransform t_destRect = targetSize;
        RectTransform t_rect = GetComponent<RectTransform>();
        Vector2 t_size = t_rect.sizeDelta;
        Vector2 t_pos = t_rect.anchoredPosition;
        OnStartCoroutine();
        while (Vector2.Distance(t_rect.sizeDelta, t_destRect.sizeDelta + new Vector2(200, 200)) >= 1)
        {
            t_rect.sizeDelta = Vector2.Lerp(t_rect.sizeDelta,
                                            t_destRect.sizeDelta + new Vector2(200, 200),
                                            6f * Time.deltaTime);
            yield return null;
        }
        t_rect.sizeDelta = t_size;
        OnEndCoroutine();
    }
}
