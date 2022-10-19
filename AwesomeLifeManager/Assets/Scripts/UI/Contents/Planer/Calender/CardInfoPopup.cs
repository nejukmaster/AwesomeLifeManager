using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoPopup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI nameBlank;
    [SerializeField] Image illustration;

    public void SetActive(bool p_bool, CardInform? p_info)
    {
        this.gameObject.SetActive(p_bool);
        if (p_bool)
        {
            nameBlank.text = p_info.name;
            description.text = p_info.description;
            Sprite t_sprite = CardManager.instance.illustrationAtlas.GetSprite(p_info.illusteName);
            if (t_sprite != null)
                illustration.sprite = t_sprite;
        }
    }
}
