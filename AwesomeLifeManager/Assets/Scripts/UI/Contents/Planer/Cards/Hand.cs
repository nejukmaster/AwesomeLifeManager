using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Vector2 anchoredPos;
    RectTransform uiCanvas;
    ActionCard[] cards = new ActionCard[4];
    [SerializeField] RectTransform[] handSlot;

    // Start is called before the first frame update
    void Start()
    {
        uiCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        Vector2 t_pos = this.GetComponent<RectTransform>().anchoredPosition;
        t_pos = new Vector2(uiCanvas.rect.width / 2, this.GetComponent<RectTransform>().anchoredPosition.y);
        anchoredPos = t_pos;
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
}
