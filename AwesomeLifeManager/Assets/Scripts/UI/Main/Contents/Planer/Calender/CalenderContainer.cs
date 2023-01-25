using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalenderContainer : MonoBehaviour
{
    public Vector2 anchored;
    RectTransform uiCanvas;

    void Awake(){
        uiCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        RectTransform t_rect = GetComponent<RectTransform>();
        anchored = uiCanvas.sizeDelta/2 - (GetComponent<RectTransform>().sizeDelta/2) + GetComponent<RectTransform>().anchoredPosition;
    }

    
}
