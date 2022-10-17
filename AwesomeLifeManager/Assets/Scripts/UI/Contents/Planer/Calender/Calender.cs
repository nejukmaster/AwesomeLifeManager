using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEditor.SearchService;

//캘린더를 구현해놓은 클래스입니다.
public class Calender : UI
{
    //캘린더 날짜 하나하나를 저장해놓은 리스트
    public CalenderCell[] cells = new CalenderCell[28];
    public List<int> checkedPlanIndexes = new List<int> ();
    //초기화시 이 클래스의 anchoredPosition을 저장해놓습니다. 이 클래스는 왠만해선 위치가 바뀌지 않으므로 RectTransform을 매번 참조하는 일을 방지하기위한 처리입니다.
    public Vector2 anchoredPos;
    public int accumFatigue = 0;
    public int accumAP = 0;
    //이 캘린더를 담고있는 오브젝트를 저장합니다. 마우스클릭이나 터치를 정규화하고 이를 캘린더 각 위치에 매핑하기 위해서 사용됩니다. 
    public CalenderContainer container;
    public bool cellsSetted;
    //CalenderCell을 담을 테두리입니다.
    [SerializeField] RectTransform frame;
    [SerializeField] RectTransform Container;
    [SerializeField] PlanDeletePopup planDeletePopup;
    RectTransform uiCanvas;
    //각 주의 y위치를 저장합니다.
    float[] weekY;
    ObjectPool theObjectPool;
    //각 주를 더블클릭시 나올 팝업창을 설정합니다.
    public WeekPlanPopup weekPlanPopup;

    // Start is called before the first frame update
    void Awake()
    {
        uiCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        anchoredPos = this.GetComponent<RectTransform>().anchoredPosition;
        container = GetComponentInParent<CalenderContainer>();
        theObjectPool = ObjectPool.instance;
        weekY = new float[4] { 2f * frame.rect.height / 4, frame.rect.height / 4, -1f * frame.rect.height / 4, -2f * frame.rect.height / 4 };
    }

    //CalenderCell을 세팅하는 함수입니다.
    public void SettingCells()
    {
        if (!cellsSetted)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    GameObject t_cell = theObjectPool.calenderCellQueue.Dequeue();
                    t_cell.GetComponent<RectTransform>().sizeDelta = new Vector2(frame.rect.width / 7, frame.rect.height / 4);
                    t_cell.GetComponent<RectTransform>().anchoredPosition = new Vector2(j * t_cell.GetComponent<RectTransform>().rect.width,
                                                                                        -1 * i * t_cell.GetComponent<RectTransform>().rect.height);
                    t_cell.GetComponentInChildren<TextMeshProUGUI>().text = (i * 7 + j + 1).ToString();
                    cells[i * 7 + j] = t_cell.GetComponent<CalenderCell>();
                    cells[i * 7 + j].calender = cells[i * 7 + j].GetComponentInParent<Calender>();
                    t_cell.SetActive(true);
                }
            }
            cellsSetted = true;
        }
    }

    public void InitialCells()
    {
        for(int i = 0; i < cells.Length; i++)
        {
            theObjectPool.calenderCellQueue.Enqueue(cells[i].gameObject);
            cells[i].gameObject.SetActive(false);
        }
        cellsSetted = false;
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
                weekPlanPopup.SetActive(true, r);
                UI.ToggleSubUI(container.gameObject, false);
            }
            return true;
        }
        return false;
    }

    public void Check(PlanBox p_planBox, bool p_bool)
    {
        if (p_bool)
            checkedPlanIndexes.Add(p_planBox.planNum);
        else
            checkedPlanIndexes.Remove(p_planBox.planNum);
    }

    public void PlanDeleteButtonClick()
    {
        planDeletePopup.SetActive(true, checkedPlanIndexes.Count + "개의 일정을 삭제하시겠습니까?");
    }
}
