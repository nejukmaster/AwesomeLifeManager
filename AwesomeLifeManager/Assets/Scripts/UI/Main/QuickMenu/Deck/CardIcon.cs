using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class CardIcon : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI nameBlank;
    [SerializeField] Image illustration;
    [SerializeField] Image categoryImg;
    [SerializeField] TextMeshProUGUI cost;
    CardWindow cardWindow;
    CardInform cardInform;

    public void SettingCard(CardInform inform)
    {
        cardWindow = GetComponentInParent<IconContainer>().cardWindow;
        cardInform = inform;
        nameBlank.text = inform.name;
        description.text = inform.description;
        cost.text = inform.cost.ToString();
        Sprite t_sprite = CardManager.instance.illustrationAtlas.GetSprite(inform.illusteName);
        if (t_sprite != null)
        {
            illustration.sprite = t_sprite;
        }
        SpriteAtlas t_atlas = GetComponentInParent<UIManager>().IconAtlas;
        switch (inform.type)
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

    public void OnClick()
    {
        cardWindow.SetActive(true, cardInform);
    }
}
