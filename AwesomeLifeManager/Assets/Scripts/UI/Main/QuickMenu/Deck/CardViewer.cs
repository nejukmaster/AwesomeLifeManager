using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewer : Scroll
{

    [SerializeField] CardType containType;
    [SerializeField] MyDeck myDeck;
    ObjectPool theObjectPool;
    [SerializeField] RectTransform containerRect;
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
                t_rect.anchoredPosition = new Vector2((i % 3) * (containerRect.rect.width / 3), -1 * Mathf.Floor(i / 3) * t_rect.rect.height + 25f);
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

    public override void onStartSwipe()
    {
        return;
    }

    public override void onEndSwipe()
    {
        return;
    }
}
