using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewer : Scroll
{

    [SerializeField] CardType containType;
    [SerializeField] MyDeck myDeck;
    ObjectPool theObjectPool;
    RectTransform containerRect;
    // Start is called before the first frame update
    void Awake()
    {
        theObjectPool = ObjectPool.instance;
        containerRect = GetComponentInParent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenIcons()
    {
        int i = 0;
        foreach(ActionCard card in myDeck.cardQueue)
        {
            if(card.inform.type == containType)
            {
                GameObject obj = theObjectPool.cardIconQueue.Dequeue();
                obj.transform.SetParent(objGroup, false);
                CardIcon icon = obj.GetComponent<CardIcon>();
                icon.SettingCard(card.inform);
                RectTransform t_rect = icon.GetComponent<RectTransform>();
                t_rect.anchoredPosition = new Vector2((i % 3) * (containerRect.rect.width / 3), Mathf.Floor(i / 3) * t_rect.rect.height + 25f);
                i++;
            }
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
