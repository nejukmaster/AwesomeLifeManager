using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionCard : UI
{
    private bool canClick = true;
    private float slideSpeed = 9.5f;
    private Vector2 anchoredPos;
    private Coroutine currentCoroutine;
    RectTransform uiCanvas;
    Hand hand;

    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI nameBlank;
    [SerializeField] Image illustration;
    
    public Calender calender;
    public bool activated = false;
    public CalenderCell currentCell;
    public CardInform inform;
    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponentInParent<Hand>();  
        uiCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    public CalenderCell CheckHolding()
    {
        try
        {
            Vector2 t_pos = this.GetComponent<RectTransform>().anchoredPosition + hand.anchoredPos;
            Vector2 s_pos = new Vector2(calender.anchoredPos.x - calender.GetComponent<RectTransform>().rect.width / 2, calender.anchoredPos.y - calender.GetComponent<RectTransform>().rect.height / 2);
            Vector2 e_pos = new Vector2(calender.anchoredPos.x + calender.GetComponent<RectTransform>().rect.width / 2, calender.anchoredPos.y + calender.GetComponent<RectTransform>().rect.height / 2);
            if (t_pos.x >= s_pos.x && t_pos.y >= s_pos.y && t_pos.x <= e_pos.x && t_pos.y <= e_pos.y)
            {
                int x_gride = (int)((t_pos.x - s_pos.x) / CalenderCell.width);
                int y_gride = (int)((e_pos.y - t_pos.y) / CalenderCell.height);
                return calender.cells[y_gride * 7 + x_gride];
            }
            return null;
        }
        catch(IndexOutOfRangeException e)
        {
            return null;
        }
    }

    public void Slide(Vector2 p_dest)
    {
        if(currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(SlideCo(p_dest));
    }

    public void SettingCard()
    {
        nameBlank.text = inform.name;
        description.text = inform.description;
        Sprite t_sprite = CardManager.instance.illustrationAtlas.GetSprite(inform.illusteName);
        if (t_sprite != null)
            illustration.sprite = t_sprite;
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
