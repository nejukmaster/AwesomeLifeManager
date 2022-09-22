using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaler : MonoBehaviour
{
    [SerializeField] Vector2 originSizeRaitio;
    [SerializeField] RectTransform standardRect;
    // Start is called before the first frame update
    void Start()
    {
         this.GetComponent<RectTransform>().sizeDelta = new Vector2(standardRect.rect.width * originSizeRaitio.x, standardRect.rect.height * originSizeRaitio.y);
    }
}
