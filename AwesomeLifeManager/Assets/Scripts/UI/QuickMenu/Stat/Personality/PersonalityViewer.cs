using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalityViewer : Scroll,Viewer
{
    ObjectPool theObjectPool;
    PersonalityManager thePersonalityManager;

    public void Awake()
    {
        theObjectPool = ObjectPool.instance;
        thePersonalityManager = PersonalityManager.instance;
    }

    void Viewer.DeclareBox()
    {
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].gameObject.SetActive(false);
            theObjectPool.personalityBoxQueue.Enqueue(objs[i].gameObject);
        }
        updateObjs<PersonalityBox>();
    }

    void Viewer.GenBox()
    {
        for (int i = 0; i < thePersonalityManager.personalities.Count; i++)
        {
            GameObject t_box = theObjectPool.personalityBoxQueue.Dequeue();
            t_box.SetActive(true);
            PersonalityBox t_personaltiy = t_box.GetComponent<PersonalityBox>();
            t_personaltiy.Setting(thePersonalityManager.personalities[i]);
            RectTransform t_rect = t_box.GetComponent<RectTransform>();
            t_rect.anchoredPosition = new Vector2(t_rect.rect.width * (i % 2), -1 * t_rect.rect.height * (i/2));
        }
        updateObjs<PersonalityBox>();
    }

    public MonoBehaviour GetObj()
    {
        return this;
    }

    public override void onEndSwipe()
    {
        return;
    }

    public override void onStartSwipe()
    {
        return;
    }
}
