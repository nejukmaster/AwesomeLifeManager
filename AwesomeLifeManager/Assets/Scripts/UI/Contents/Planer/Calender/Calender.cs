using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calender : UI
{
    public CalenderCell[] cells = new CalenderCell[28];
    public Vector2 anchoredPos;
    public int accumFatigue = 0;
    public int accumAP = 0;
    [SerializeField] RectTransform frame;
    [SerializeField] RectTransform Container;
    RectTransform uiCanvas;
    CalenderContainer container;
    float[] weekY;
    public CalenderCellWindow cellInspectorPopup;

    // Start is called before the first frame update
    void Awake()
    {
        uiCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        anchoredPos = this.GetComponent<RectTransform>().anchoredPosition;
        container = GetComponentInParent<CalenderContainer>();
        weekY = new float[4] { 2f * frame.rect.height / 4, frame.rect.height / 4, -1f * frame.rect.height / 4, -2f * frame.rect.height / 4 };
    }

    public void SettingCells()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                GameObject t_cell = ObjectPool.instance.calenderCellQueue.Dequeue();
                t_cell.GetComponent<RectTransform>().sizeDelta = new Vector2(frame.rect.width / 7, frame.rect.height / 4);
                t_cell.GetComponent<RectTransform>().anchoredPosition = new Vector2(j * t_cell.GetComponent<RectTransform>().rect.width,
                                                                                    -1 * i * t_cell.GetComponent<RectTransform>().rect.height);
                t_cell.GetComponentInChildren<TextMeshProUGUI>().text = (i * 7 + j + 1).ToString();
                cells[i * 7 + j] = t_cell.GetComponent<CalenderCell>();
                cells[i * 7 + j].calender = cells[i * 7 + j].GetComponentInParent<Calender>();
                t_cell.SetActive(true);
            }
        }
    }

    public override bool onClickDown(Vector2 clickPos)
    {
        return false;
    }

    public override bool onClickUp(float dragDis, Vector2 clickPos)
    {
        return false;
    }

    public override bool onSwipe(Vector2 swipeStartp, Vector2 swipeEndp)
    {
        return false;
    }

    public override bool onDoubleClick(Vector2 clickPos, bool isActivate)
    {
        Vector2 t_pos = new Vector2(Utility.Mapping(clickPos.x, new Vector2(0, Screen.width), new Vector2(0, uiCanvas.rect.width)),
                                        Utility.Mapping(clickPos.y, new Vector2(0, Screen.height), new Vector2(0, uiCanvas.rect.height)));
        t_pos = (t_pos - container.anchored) - anchoredPos;
        if (t_pos.x <= frame.rect.width / 2 && t_pos.x >= -1f * frame.rect.width / 2 &&
            t_pos.y <= frame.rect.height / 2 && t_pos.y >= -1f * frame.rect.height / 2)
        {
            if (isActivate)
            {
                int r = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (Math.Abs(t_pos.y - weekY[i]) < Math.Abs(t_pos.y - weekY[r]))
                    {
                        r = i;
                    }
                }
                Debug.Log(r);
            }
            return true;
        }
        return false;
    }
}
