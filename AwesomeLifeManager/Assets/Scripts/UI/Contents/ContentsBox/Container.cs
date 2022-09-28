using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Container : MonoBehaviour
{
    [SerializeField] RectTransform upperTarget;
    [SerializeField] RectTransform downerTarget;

    [SerializeField] RectTransform mask;
    [SerializeField] RectTransform upperMaskTarget;
    [SerializeField] RectTransform downerMaskTarget;

    [SerializeField] RectTransform characterUI;
    [SerializeField] RectTransform upperCharacterTarget;
    [SerializeField] RectTransform downerCharacterTarget;
    Coroutine co;

    public void ZoomIn(bool p_anime)
    {
        if (co != null)
            StopCoroutine(co);
        co = StartCoroutine(ZoomInCo(p_anime));
    }

    public void ZoomOut(bool p_anime)
    {
        if (co != null)
            StopCoroutine(co);
        co = StartCoroutine(ZoomOutCo(p_anime));
    }

    public IEnumerator ZoomInCo(bool p_anime)
    {
        RectTransform t_rect = GetComponent<RectTransform>();
        while((Vector2.Distance(t_rect.anchoredPosition, upperTarget.anchoredPosition) >= 0.3 || Vector2.Distance(t_rect.sizeDelta, upperTarget.sizeDelta) >= 0.5) && p_anime)
        {
            t_rect.anchoredPosition = Vector2.Lerp(t_rect.anchoredPosition,
                                                    upperTarget.anchoredPosition,
                                                    4f * Time.deltaTime);
            t_rect.sizeDelta = Vector2.Lerp(t_rect.sizeDelta,
                                                    upperTarget.sizeDelta,
                                                    10f * Time.deltaTime);
            mask.sizeDelta = Vector2.Lerp(mask.sizeDelta,
                                                    upperMaskTarget.sizeDelta,
                                                    10f * Time.deltaTime);
            characterUI.anchoredPosition = Vector2.Lerp(characterUI.anchoredPosition,
                                                    upperCharacterTarget.anchoredPosition,
                                                    4f * Time.deltaTime);
            yield return null;
        }
        t_rect.anchoredPosition = upperTarget.anchoredPosition;
        t_rect.sizeDelta = upperTarget.sizeDelta;
        mask.sizeDelta = upperMaskTarget.sizeDelta;
        characterUI.anchoredPosition = upperCharacterTarget.anchoredPosition;
    }

    public IEnumerator ZoomOutCo(bool p_anime)
    {
        RectTransform t_rect = GetComponent<RectTransform>();
        while ((Vector2.Distance(t_rect.anchoredPosition, downerTarget.anchoredPosition) >= 0.3 || Vector2.Distance(t_rect.sizeDelta, downerTarget.sizeDelta) >= 0.5) && p_anime)
        {
            t_rect.anchoredPosition = Vector2.Lerp(t_rect.anchoredPosition,
                                                    downerTarget.anchoredPosition,
                                                    4f * Time.deltaTime);
            t_rect.sizeDelta = Vector2.Lerp(t_rect.sizeDelta,
                                                    downerTarget.sizeDelta,
                                                    10f * Time.deltaTime);
            mask.sizeDelta = Vector2.Lerp(mask.sizeDelta,
                                                    downerMaskTarget.sizeDelta,
                                                    10f * Time.deltaTime);
            characterUI.anchoredPosition = Vector2.Lerp(characterUI.anchoredPosition,
                                                    downerCharacterTarget.anchoredPosition,
                                                    4f * Time.deltaTime);
            yield return null;
        }
        t_rect.anchoredPosition = downerTarget.anchoredPosition;
        t_rect.sizeDelta = downerTarget.sizeDelta;
        mask.sizeDelta = downerMaskTarget.sizeDelta;
        characterUI.anchoredPosition = downerCharacterTarget.anchoredPosition;
    }
}
