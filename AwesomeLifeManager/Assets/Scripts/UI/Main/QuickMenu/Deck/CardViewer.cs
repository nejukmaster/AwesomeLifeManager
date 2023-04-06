using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardViewer : Scroll
{
    public bool selectionMode = false;

    [SerializeField] CardType containType;
    [SerializeField] MyDeck myDeck;
    ObjectPool theObjectPool;
    [SerializeField] RectTransform containerRect;
    [SerializeField] GameObject top;
    // Start is called before the first frame update
    void Awake()
    {
        theObjectPool = ObjectPool.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenIcons()
    {
        int i = 0;
        foreach(CardInform inform in myDeck.cardInformList)
        {
            if(inform.type == containType)
            {
                GameObject obj = theObjectPool.cardIconQueue.Dequeue();
                obj.SetActive(true);
                obj.transform.SetParent(objGroup, false);
                CardIcon icon = obj.GetComponent<CardIcon>();
                icon.SettingCard(inform);
                RectTransform t_rect = icon.GetComponent<RectTransform>();
                t_rect.anchoredPosition = new Vector2((i % 3) * (containerRect.rect.width / 3), -1 * Mathf.Floor(i / 3) * t_rect.rect.height);
                i++;
            }
        }
        updateObjs<CardIcon>();
    }

    public void DeleteIcons()
    {
        foreach(RectTransform r in objs)
        {
            theObjectPool.cardIconQueue.Enqueue(r.gameObject);
            r.gameObject.SetActive(false);
            updateObjs<CardIcon>();
        }
    }

    public void activateSelectionMode()
    {
        selectionMode = true;
        CardIcon[] t_icons = GetComponentsInChildren<CardIcon>();
        Button[] t_btns = top.GetComponentsInChildren<Button>();
        foreach(Button i in t_btns)
        {
            i.enabled = false;
        }
    }
    public void deactivateSelectionMode()
    {
        selectionMode = false;
        CardIcon[] t_icons = GetComponentsInChildren<CardIcon>();
        Button[] t_btns = top.GetComponentsInChildren<Button>();
        foreach (Button i in t_btns)
        {
            i.enabled = true;
        }
    }

    public override void onStartSwipe()
    {
        return;
    }

    public override void onEndSwipe()
    {
        return;
    }
}
