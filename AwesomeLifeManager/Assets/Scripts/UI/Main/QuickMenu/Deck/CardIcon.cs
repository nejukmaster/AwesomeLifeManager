using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class CardIcon : MonoBehaviour
{
    public const float LONG_CLICK_SEC = 1.5f;

    public bool isCheck = false;
    public bool canDelete = true;

    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI nameBlank;
    [SerializeField] Image illustration;
    [SerializeField] Image categoryImg;
    [SerializeField] TextMeshProUGUI cost;
    CardWindow cardWindow;
    public CardInform cardInform;
    bool hold = false;
    public float holdTime = 0.0f;

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
            case CardType.Effect:
                categoryImg.sprite = t_atlas.GetSprite("Effect");
                break;
        }
    }

    public void OnClick()
    {
           cardWindow.SetActive(true, cardInform);
    }

    public void OnClickDown()
    {
        hold = true;
    }

    public void Update()
    {
        if (hold)
        {
            holdTime += Time.deltaTime;
        }
    }
}
