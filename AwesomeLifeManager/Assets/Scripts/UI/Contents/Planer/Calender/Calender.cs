using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calender : MonoBehaviour
{
    public CalenderCell[] cells = new CalenderCell[28];
    public Vector2 anchoredPos;
    public int accumFatigue = 0;
    public int accumAP = 0;
    [SerializeField] RectTransform frame;
    RectTransform uiCanvas;
    public CalenderCellWindow cellInspectorPopup;

    // Start is called before the first frame update
    void Awake()
    {
        uiCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        anchoredPos = this.GetComponent<RectTransform>().anchoredPosition;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
