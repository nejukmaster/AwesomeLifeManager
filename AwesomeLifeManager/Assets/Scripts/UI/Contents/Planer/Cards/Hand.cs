using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Hand : UI
{
    public Vector2 anchoredPos;
    RectTransform uiCanvas;
    ActionCard[] cards = new ActionCard[4];
    int selectedCardIndex;
    bool draging = false;
    [SerializeField] RectTransform[] handSlot;
    [SerializeField] Calender calender;
    [SerializeField] MyDeck myDeck;

    // Start is called before the first frame update
    void Awake()
    {
        uiCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        Vector2 t_pos = this.GetComponent<RectTransform>().anchoredPosition;
        t_pos = new Vector2(uiCanvas.rect.width / 2, this.GetComponent<RectTransform>().anchoredPosition.y);
        anchoredPos = t_pos;
        for(int i = 0; i < cards.Length; i++)
        {
            Draw();
        }
        selectedCardIndex = -1;
    }

    public void AddCard(ActionCard p_card)
    {
        for(int i = 0; i < cards.Length; i++)
        {
            if (cards[i] == null)
            {
                cards[i] = p_card;
                p_card.Slide(handSlot[i].anchoredPosition);
                UIManager.instance.UI_List.Add(p_card);
                return;
            }
        }
        throw new System.Exception("Hand is FULL!");
    }

    public void Draw()
    {
        if (myDeck.cardQueue != null && myDeck.cardQueue.Count > 0)
        {
            ActionCard t_card = myDeck.cardQueue.Dequeue();
            t_card.gameObject.SetActive(true);
            t_card.calender = calender;
            t_card.SettingCard();
            AddCard(t_card);
        }
    }

    public override bool onClickDown(Vector2 clickPos)
    {
        int r = -1;
        GetClickedCard(clickPos, ref r);

        if (r == -1)
        {
            return false;
        }
        else
        {
            if(selectedCardIndex != r)
            {
                return false;
            }
            else if(selectedCardIndex == r)
            {
                draging = true;
                return true;
            }
            return false;
        }
    }

    public override bool onClickUp(float dragDis, Vector2 clickPos)
    {
        int r = -1;
        GetClickedCard(clickPos, ref r);

        if (r == -1)
        {
            return false;
        }
        else
        {
            if(selectedCardIndex != r)
            {
                if(selectedCardIndex != -1)
                {
                    cards[selectedCardIndex].Slide(handSlot[selectedCardIndex].anchoredPosition);
                }
                selectedCardIndex = r;
                cards[selectedCardIndex].Slide(handSlot[r].anchoredPosition + new Vector2(0, 150));
            }
            else
            {
                if (draging)
                {
                    draging = false;
                    if (cards[selectedCardIndex].currentCell == null)
                    {
                        cards[selectedCardIndex].Slide(handSlot[selectedCardIndex].anchoredPosition);
                        selectedCardIndex = -1;
                        return true;
                    }
                    cards[selectedCardIndex].inform.actioin.actionDel(cards[selectedCardIndex].currentCell);
                    cards[selectedCardIndex].currentCell.HoldOut();
                    cards[selectedCardIndex].gameObject.SetActive(false);
                    ObjectPool.instance.actionCardQueue.Enqueue(cards[selectedCardIndex].gameObject);
                    cards[selectedCardIndex] = null;
                    selectedCardIndex = -1;
                    Draw();
                    return true;
                }
                else
                    return false;
            }
            return true;
        }
    }

    public override bool onSwipe(Vector2 swipeStartp, Vector2 swipeEndp)
    {
        if (draging)
        {
            cards[selectedCardIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(Utility.Mapping(Input.mousePosition.x, new Vector2(0, Screen.width), new Vector2(0, uiCanvas.rect.width)),
                                                                                Utility.Mapping(Input.mousePosition.y, new Vector2(0, Screen.height), new Vector2(0, uiCanvas.rect.height)))
                                                                  - anchoredPos;
            CalenderCell t_cell = cards[selectedCardIndex].currentCell;
            if (cards[selectedCardIndex].CheckHolding() != null && cards[selectedCardIndex].CheckHolding() != t_cell)
            {
                if (t_cell != null)
                    t_cell.HoldOut();
                t_cell = cards[selectedCardIndex].CheckHolding();
                t_cell.HoldeOn();
                cards[selectedCardIndex].currentCell = t_cell;
            }
            return true;
        }
        return false;
    }

    void GetClickedCard(Vector2 clickPos, ref int r)
    {
        Vector2 t_pos = new Vector2(Utility.Mapping(clickPos.x, new Vector2(0, Screen.width), new Vector2(0, uiCanvas.rect.width)),
                                        Utility.Mapping(clickPos.y, new Vector2(0, Screen.height), new Vector2(0, uiCanvas.rect.height)))
                             - anchoredPos;
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i] != null)
            {
                if (Vector2.Distance(cards[i].GetComponent<RectTransform>().anchoredPosition, t_pos) < 120)
                {
                    if (r == -1)
                    {
                        r = i;
                    }
                    else if (Vector2.Distance(cards[i].GetComponent<RectTransform>().anchoredPosition, t_pos) < Vector2.Distance(cards[r].GetComponent<RectTransform>().anchoredPosition, t_pos))
                    {
                        r = i;
                    }
                }
            }
        }
    }
}
