using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCard : MonoBehaviour
{
    private string cardImagePath = "Images/Action/Illustration";
    private bool trigger = false;
    RectTransform uiCanvas;
    Hand hand;
    [SerializeField] Calender calender;

    public bool activated = false;
    public CalenderCell currentCell;
    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponentInParent<Hand>();  
        uiCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2(Utility.Mapping(Input.mousePosition.x,new Vector2(0,Screen.width),new Vector2(0,uiCanvas.rect.width)), 
                                                                                Utility.Mapping(Input.mousePosition.y,new Vector2(0,Screen.height),new Vector2(0,uiCanvas.rect.height)))
                                                                  - hand.anchoredPos;
            if(CheckHolding() != null && CheckHolding() != currentCell)
            {
                if(currentCell != null)
                    currentCell.HoldOut();
                currentCell = CheckHolding();
                currentCell.HoldeOn();
            }
        }
    }

    CalenderCell CheckHolding()
    {
        Vector2 t_pos = this.GetComponent<RectTransform>().anchoredPosition + hand.anchoredPos;
        Vector2 s_pos = new Vector2(calender.anchoredPos.x - calender.GetComponent<RectTransform>().rect.width / 2, calender.anchoredPos.y - calender.GetComponent<RectTransform>().rect.height / 2);
        Vector2 e_pos = new Vector2(calender.anchoredPos.x + calender.GetComponent<RectTransform>().rect.width / 2, calender.anchoredPos.y + calender.GetComponent<RectTransform>().rect.height / 2);
        if(t_pos.x >= s_pos.x && t_pos.y >= s_pos.y && t_pos.x <= e_pos.x && t_pos.y <= e_pos.y)
        {
            int x_gride = (int)((t_pos.x - s_pos.x) / CalenderCell.width);
            int y_gride = (int)((e_pos.y - t_pos.y) / CalenderCell.height);
            return calender.cells[y_gride * 7 + x_gride];
        }
        return null;
    }

    public void OnClick()
    {
        if (!trigger)
        {
            activated = true;
        }
        else
        {
            activated = false;
            currentCell.InsertPlan(PlanManager.instance.planDic["00"]);
            currentCell.HoldOut();
            gameObject.SetActive(false);
        }
        trigger = !trigger;
    }

}
