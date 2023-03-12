using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Hand : UI
{
    public MyDeck myDeck;
    public Vector2 anchoredPos;
    RectTransform uiCanvas;
    EventManager theEventManager;
    ActionCard[] cards = new ActionCard[7];
    int selectedCardIndex;
    bool draging = false;
    int handAmount = 4;
    [SerializeField] RectTransform[] handSlot;
    [SerializeField] Calender calender;
    [SerializeField] CardInfoPopup cardInfoPopup;
    [SerializeField] RectTransform cardBurnedTransform;

    // Start is called before the first frame update
    void Awake()
    {
        uiCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        theEventManager = EventManager.instance;
        Vector2 t_pos = this.GetComponent<RectTransform>().anchoredPosition;
        t_pos = new Vector2(uiCanvas.rect.width / 2, this.GetComponent<RectTransform>().anchoredPosition.y);
        anchoredPos = t_pos;
        selectedCardIndex = -1;
    }

    public void SettingHand(bool p_bool)
    {
        if (p_bool)
        {
            for (int i = 0; i < handAmount; i++)
            {
                Draw();
            }
        }
        else
        {
            Queue<ActionCard> t_Queue = new Queue<ActionCard>(myDeck.cardQueue.Reverse<ActionCard>());
            int j = 0;
            for(int i = 0; i < cards.Length; i++)
            {
                if (cards[i] != null)
                {
                    t_Queue.Enqueue(cards[i]);
                    cards[i].gameObject.SetActive(false);
                    cards[i].calender = null;
                    cards[i] = null;
                    j++;
                }
            }
            myDeck.cardQueue = new Queue<ActionCard>(t_Queue.Reverse<ActionCard>());
            handAmount = j;
            selectedCardIndex = -1;
        }
    }

    public void InitHand()
    {
        handAmount = 4;
        selectedCardIndex = -1;
    }

    public void AddCard(ActionCard p_card)
    {
        for(int i = 0; i < cards.Length; i++)
        {
            if (i < handSlot.Length && cards[i] == null)
            {
                cards[i] = p_card;
                p_card.Slide(handSlot[i].anchoredPosition,null);
                return;
            }
        }
        if (selectedCardIndex != -1)
        {
            cards[selectedCardIndex].Slide(handSlot[selectedCardIndex].anchoredPosition);
        }
        cardInfoPopup.SetActive(false, null);
        selectedCardIndex = -1;
        p_card.Slide(cardBurnedTransform.anchoredPosition,(x) => { p_card.Burn(); });
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
                cardInfoPopup.SetActive(false, null);
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
                cardInfoPopup.SetActive(true, cards[selectedCardIndex].inform);
            }
            else
            {
                if (draging)
                {
                    draging = false;
                    if (cards[selectedCardIndex].currentCell == null || dragDis <= 10f)
                    {
                        if (cards[selectedCardIndex].currentCell != null)
                        {
                            cards[selectedCardIndex].currentCell.HoldOut();
                            cards[selectedCardIndex].Slide(handSlot[selectedCardIndex].anchoredPosition);

                        }
                        selectedCardIndex = -1;
                        return true;
                    }
                    if (cards[selectedCardIndex].inform.type == CardType.Action)
                    {
                        if (cards[selectedCardIndex].inform.action.actionDel(cards[selectedCardIndex].currentCell, cards[selectedCardIndex].inform))
                        {
                            cards[selectedCardIndex].Burn();
                            calender.fatiguePreview.Setting();
                            ObjectPool.instance.actionCardQueue.Enqueue(cards[selectedCardIndex].gameObject);
                            cards[selectedCardIndex].currentCell.HoldOut();
                            cards[selectedCardIndex] = null;
                            Draw();
                        }
                        else
                        {
                            cards[selectedCardIndex].Slide(handSlot[selectedCardIndex].anchoredPosition);
                            cards[selectedCardIndex].currentCell.HoldOut();
                        }
                    }
                    else if(cards[selectedCardIndex].inform.type == CardType.Effect)
                    {
                        cards[selectedCardIndex].inform.action.actionDel(theEventManager, cards[selectedCardIndex].inform);
                        cards[selectedCardIndex].Burn();
                        ObjectPool.instance.actionCardQueue.Enqueue(cards[selectedCardIndex].gameObject);
                        cards[selectedCardIndex] = null;
                        Draw();
                    }
                    selectedCardIndex = -1;
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
                if (Utility.GetNullArrayLength(t_cell.insertedPlan) < 7 || cards[selectedCardIndex].inform.type != CardType.Action)
                {
                    if (cards[selectedCardIndex].inform.type == CardType.Action)
                    {
                        t_cell.HoldeOn();
                    }
                    cards[selectedCardIndex].currentCell = t_cell;
                }
                else
                {
                    cards[selectedCardIndex].currentCell = null;
                }
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
