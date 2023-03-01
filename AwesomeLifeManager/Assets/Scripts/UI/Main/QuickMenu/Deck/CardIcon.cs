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

    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI nameBlank;
    [SerializeField] Image illustration;
    [SerializeField] Image categoryImg;
    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] Image checkMarker;
    [SerializeField] GameObject selectUI;
    CardWindow cardWindow;
    CardInform cardInform;
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
        if(GetComponentInParent<CardViewer>().selectionMode == false)
        {
            if (holdTime <= 0.2)
            {
                cardWindow.SetActive(true, cardInform);
            }
        }
        else{
            isCheck = !isCheck;
            if (isCheck)
            {
                checkMarker.enabled = true;
            }
            else
                checkMarker.enabled = false;
        }
    }

    public void OnClickDown()
    {
        hold = true;
    }

    public void OnClickUp()
    {
        if(holdTime >= LONG_CLICK_SEC)
        {
            GetComponentInParent<CardViewer>().activateSelectionMode();
        }
        holdTime = 0.0f;
        hold = false;
    }

    public void activateSelectionMode()
    {
        selectUI.SetActive(true);
    }

    public void deactivateSelectionMode()
    {
        selectUI.SetActive(false);
        isCheck = false;
        checkMarker.enabled = false;
    }

    public void Update()
    {
        if (hold)
        {
            holdTime += Time.deltaTime;
        }
    }
}
