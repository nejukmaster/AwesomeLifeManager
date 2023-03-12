using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class CardInfoPopup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI nameBlank;
    [SerializeField] Image illustration;
    [SerializeField] Image categoryImg;
    [SerializeField] TextMeshProUGUI cost;

    public void SetActive(bool p_bool, CardInform? p_info)
    {
        this.gameObject.SetActive(p_bool);
        if (p_bool)
        {
            nameBlank.text = p_info.name;
            description.text = p_info.description;
            cost.text = p_info.cost.ToString();
            Sprite t_sprite = CardManager.instance.illustrationAtlas.GetSprite(p_info.illusteName);
            if (t_sprite != null)
                illustration.sprite = t_sprite;
            SpriteAtlas t_atlas = GetComponentInParent<UIManager>().IconAtlas;
            switch (p_info.type)
            {
                case CardType.Action:
                    categoryImg.sprite = t_atlas.GetSprite("Action");
                    break;
                case CardType.Effect:
                    categoryImg.sprite = t_atlas.GetSprite("Effect");
                    break;
            }
        }
    }
}
