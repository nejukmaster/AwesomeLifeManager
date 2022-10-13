using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIReplacer : MonoBehaviour
{
    //9:16비율에서 작업한 UI를 다른 해상도에 맞게 재배치해줍니다.
    Vector2 standardUIRect = new Vector2(900f,1600f);
    bool replaced = false;
    void Start()
    {
        if (!replaced)
        {
            RectTransform UI_rect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.GetComponent<RectTransform>().anchoredPosition.x * (UI_rect.rect.width / standardUIRect.x), this.GetComponent<RectTransform>().anchoredPosition.y * (UI_rect.rect.height / standardUIRect.y));
            replaced = true;
        }
    }

    public void PreReplacing()
    {
            RectTransform UI_rect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.GetComponent<RectTransform>().anchoredPosition.x * (UI_rect.rect.width / standardUIRect.x), this.GetComponent<RectTransform>().anchoredPosition.y * (UI_rect.rect.height / standardUIRect.y));
            replaced=true;
    }
}
