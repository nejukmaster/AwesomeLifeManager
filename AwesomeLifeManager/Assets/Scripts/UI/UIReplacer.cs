using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//??? UI? ????? ?? ????? ???
public class UIReplacer : MonoBehaviour
{
    Vector2 standardUIRect = new Vector2(900f,1600f);

    void Start()
    {
        RectTransform UI_rect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.GetComponent<RectTransform>().anchoredPosition.x * (UI_rect.rect.width / standardUIRect.x), this.GetComponent<RectTransform>().anchoredPosition.y * (UI_rect.rect.height / standardUIRect.y));
    }
}
