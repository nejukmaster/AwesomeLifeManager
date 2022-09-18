using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calender : MonoBehaviour
{
    public Vector2 anchoredPos;
    RectTransform uiCanvas;
    // Start is called before the first frame update
    void Awake()
    {
        uiCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(uiCanvas.rect.width / 2, uiCanvas.rect.height / 2);
        anchoredPos = this.GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
