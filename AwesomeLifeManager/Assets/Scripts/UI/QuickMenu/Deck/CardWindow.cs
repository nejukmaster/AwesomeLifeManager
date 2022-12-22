using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class CardWindow : MonoBehaviour
{
    [SerializeField] GameObject disableUI;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI nameBlank;
    [SerializeField] Image illustration;
    [SerializeField] Image categoryImg;
    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] GameObject front;
    [SerializeField] GameObject back;
    [SerializeField] Vector2 destSize;
    Coroutine coroutine;
    bool fliped = false;
    bool canClick = false;

    public void OnClick()
    {
        if (canClick)
        {
            SetActive(false, null);
        }
    }

    public void SetActive(bool p_bool, CardInform p_inform)
    {
        
        if (p_bool)
        {
            this.gameObject.SetActive(p_bool);
            front.SetActive(false);
            back.SetActive(true);
            nameBlank.text = p_inform.name;
            description.text = p_inform.description;
            cost.text = p_inform.cost.ToString();
            Sprite t_sprite = CardManager.instance.illustrationAtlas.GetSprite(p_inform.illusteName);
            if (t_sprite != null)
            {
                illustration.sprite = t_sprite;
            }
            SpriteAtlas t_atlas = GetComponentInParent<UIManager>().IconAtlas;
            switch (p_inform.type)
            {
                case CardType.Action:
                    categoryImg.sprite = t_atlas.GetSprite("Action");
                    break;
                case CardType.Project:
                    categoryImg.sprite = t_atlas.GetSprite("Project");
                    break;
                case CardType.Event:
                    categoryImg.sprite = t_atlas.GetSprite("Event");
                    break;
                case CardType.Angel:
                    categoryImg.sprite = t_atlas.GetSprite("Angel");
                    break;
            }
            Flip();
        }
        else
        {
            Unflip();
        }
    }

    public void Flip()
    {
        UI.ToggleSubUI(disableUI, false);
        StartCoroutine(FlipCo());
    }

    public void Unflip()
    {
        UI.ToggleSubUI(disableUI, true);
        StartCoroutine(UnflipCo());
    }

    IEnumerator FlipCo()
    {
        canClick = false;
        RectTransform t_rect = (RectTransform)this.transform;
        while(t_rect.localScale.x >= 0.1)
        {
            t_rect.localScale = Vector2.Lerp(t_rect.localScale,
                                            new Vector2(0,t_rect.localScale.y),
                                            3f * Time.deltaTime);
            yield return null;
        }
        t_rect.localScale = new Vector2(0, t_rect.localScale.y);
        back.SetActive(false);
        front.SetActive(true);
        while (destSize.x - t_rect.localScale.x >= 0.1)
        {
            t_rect.localScale = Vector2.Lerp(t_rect.localScale,
                                            destSize,
                                            3f * Time.deltaTime);
            yield return null;
        }
        t_rect.localScale = destSize;
        fliped = true;
        canClick = true;
        yield return null;
    }

    IEnumerator UnflipCo()
    {
        canClick = false;
        RectTransform t_rect = (RectTransform)this.transform;
        while (t_rect.localScale.x >= 0.1)
        {
            t_rect.localScale = Vector2.Lerp(t_rect.localScale,
                                            new Vector2(0, t_rect.localScale.y),
                                            3f * Time.deltaTime);
            yield return null;
        }
        t_rect.localScale = new Vector2(0, t_rect.localScale.y);
        back.SetActive(true);
        front.SetActive(false);
        while (destSize.x - t_rect.localScale.x >= 0.1)
        {
            t_rect.localScale = Vector2.Lerp(t_rect.localScale,
                                            destSize,
                                            3f * Time.deltaTime);
            yield return null;
        }
        t_rect.localScale = destSize;
        fliped = false;
        canClick = true;
        gameObject.SetActive(false);
    }
}
