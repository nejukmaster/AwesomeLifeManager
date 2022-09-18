using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Vector2 anchoredPos;
    RectTransform uiCanvas;
    List<ActionCard> cards = new List<ActionCard>();

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
        cards.Add(p_card);
    }
}
