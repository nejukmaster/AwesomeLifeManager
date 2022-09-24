using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : UI
{
    public Vector2 anchoredPos;
    RectTransform uiCanvas;
    ActionCard[] cards = new ActionCard[4];
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
            
        }
    }

    public void AddCard(ActionCard p_card)
    {
        for(int i = 0; i < cards.Length; i++)
        {
            if (cards[i] == null)
            {
                cards[i] = p_card;
                StartCoroutine(p_card.SlideCo(handSlot[i].anchoredPosition));
                return;
            }
        }
        throw new System.Exception("Hand is FULL!");
    }

    public void Draw()
    {
        GameObject t_card = ObjectPool.instance.actionCardQueue.Dequeue();
        UIManager.instance.UI_List.Add(t_card.GetComponent<ActionCard>());
        t_card.SetActive(true);
        //StartCoroutine(t_card.GetComponent<ActionCard>().SlideCo(handSlot[i].anchoredPosition));
        t_card.GetComponent<ActionCard>().calender = calender;
        AddCard(t_card.GetComponent<ActionCard>());
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
}
