using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//UI크기를 비율에 맞춰 재조정합니다.
public class UIScaler : MonoBehaviour
{
    [SerializeField] Vector2 originSizeRaitio;
    [SerializeField] RectTransform standardRect;
    [SerializeField] bool scaleScaling = false;
    bool resized = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!resized)
        {
            if (!scaleScaling)
                this.GetComponent<RectTransform>().sizeDelta = new Vector2(standardRect.rect.width * originSizeRaitio.x, standardRect.rect.height * originSizeRaitio.y);
            else
                this.GetComponent<RectTransform>().localScale = new Vector2(standardRect.rect.width * originSizeRaitio.x, standardRect.rect.height * originSizeRaitio.y);
            resized = true;
        }
    }

    public void PreResizing()
    {
        if (!resized)
        {
            if(!scaleScaling)
                this.GetComponent<RectTransform>().sizeDelta = new Vector2(standardRect.rect.width * originSizeRaitio.x, standardRect.rect.height * originSizeRaitio.y);
            else
                this.GetComponent<RectTransform>().localScale = new Vector2(standardRect.rect.width * originSizeRaitio.x, standardRect.rect.height * originSizeRaitio.y);
            resized = true;
        }
    }
}
