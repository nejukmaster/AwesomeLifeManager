using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calender : MonoBehaviour
{
    public RectTransform[] cells = new RectTransform[28];
    public Vector2 anchoredPos;
    RectTransform uiCanvas;
    // Start is called before the first frame update
    void Awake()
    {
        uiCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(uiCanvas.rect.width / 2, uiCanvas.rect.height / 2);
        anchoredPos = this.GetComponent<RectTransform>().anchoredPosition;
        for (int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 7; j++)
            {
                GameObject t_cell = ObjectPool.instance.calenderCellQueue.Dequeue();
                t_cell.GetComponent<RectTransform>().anchoredPosition = new Vector2(j * t_cell.GetComponent<RectTransform>().rect.width,
                                                                                    -1 * i * t_cell.GetComponent<RectTransform>().rect.height);
                t_cell.GetComponentInChildren<TextMeshProUGUI>().text = (i * 7 + j +1).ToString();
                cells[i * 7 + j] = t_cell.GetComponent<RectTransform>();
                t_cell.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}