using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//??? UI? ????? ?? ???? ??
public class UIScaler : MonoBehaviour
{
    [SerializeField] Vector2 originSizeRaitio;
    [SerializeField] RectTransform standardRect;
    bool resized = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!resized)
        {
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(standardRect.rect.width * originSizeRaitio.x, standardRect.rect.height * originSizeRaitio.y);
            resized = true;
        }
    }

    public void PreResizing()
    {
        if (!resized)
        {
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(standardRect.rect.width * originSizeRaitio.x, standardRect.rect.height * originSizeRaitio.y);
            resized = true;
        }
    }
}
