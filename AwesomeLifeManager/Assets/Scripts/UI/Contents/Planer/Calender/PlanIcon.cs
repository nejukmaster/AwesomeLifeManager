using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PlanIcon : MonoBehaviour
{
    [SerializeField] Image categoryImg;
    public void SetAcitve(bool p_bool,CardType p_type )
    {
        this.gameObject.SetActive(p_bool);
        SpriteAtlas t_atlas = GetComponentInParent<UIManager>().IconAtlas;
        switch (p_type)
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
    }
    public void SetPibot(int p_pibot)
    {
        RectTransform t_rect = this.GetComponent<RectTransform>();
        RectTransform cell_rect = this.GetComponentInParent<CalenderCell>().GetComponent<RectTransform>();
        t_rect.anchoredPosition = new Vector2(cell_rect.rect.width * p_pibot / 7, 0);
    }
}
