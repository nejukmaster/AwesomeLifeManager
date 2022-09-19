using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCard : UI
{
    private string cardImagePath = "Images/Action/Illustration";
    private bool canClick = true;
    private float slideSpeed = 9.5f;
    private Vector2 anchoredPos;
    RectTransform uiCanvas;
    Hand hand;
    
    public Calender calender;
    public bool activated = false;
    public CalenderCell currentCell;
    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponentInParent<Hand>();  
        uiCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
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

    public override bool onClickDown(Vector2 clickPos)
    {
        if (canClick)
        {
            FindObjectOfType<PlanerCloseButton>().GetComponent<Button>().enabled = false;
            Vector2 t_pos = new Vector2(Utility.Mapping(clickPos.x, new Vector2(0, Screen.width), new Vector2(0, uiCanvas.rect.width)),
                                        Utility.Mapping(clickPos.y, new Vector2(0, Screen.height), new Vector2(0, uiCanvas.rect.height)))
                             - hand.anchoredPos;
            if (Vector2.Distance(GetComponent<RectTransform>().anchoredPosition, t_pos) <= 50)
            {
                activated = true;
                return true;
            }
        }
        return false;
    }

    public override bool onClickUp(float dragDis, Vector2 clickPos)
    {
        if (activated)
        {
            FindObjectOfType<PlanerCloseButton>().GetComponent<Button>().enabled = true;
            activated = false;
            currentCell.InsertPlan(PlanManager.instance.planDic["00"]);
            currentCell.HoldOut();
            gameObject.SetActive(false);
            return true;
        }
        else
            return false;
    }

    public override bool onSwipe(Vector2 swipeStartp, Vector2 swipeEndp)
    {
        if (activated)
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(Utility.Mapping(Input.mousePosition.x, new Vector2(0, Screen.width), new Vector2(0, uiCanvas.rect.width)),
                                                                                Utility.Mapping(Input.mousePosition.y, new Vector2(0, Screen.height), new Vector2(0, uiCanvas.rect.height)))
                                                                  - hand.anchoredPos;
            if (CheckHolding() != null && CheckHolding() != currentCell)
            {
                if (currentCell != null)
                    currentCell.HoldOut();
                currentCell = CheckHolding();
                currentCell.HoldeOn();
            }
            return true;
        }
        return false;
    }

    public IEnumerator SlideCo(Vector2 p_dest)
    {
        while(Vector2.Distance(GetComponent<RectTransform>().anchoredPosition,p_dest) >= 0.1)
        {
            canClick = false;
            RectTransform t_rect = GetComponent<RectTransform>();
            t_rect.anchoredPosition = Vector2.Lerp(t_rect.anchoredPosition,
                                                    p_dest,
                                                    slideSpeed * Time.deltaTime);
            yield return null;
        }
        GetComponent<RectTransform>().anchoredPosition = p_dest;
        canClick = true;
    }
}
