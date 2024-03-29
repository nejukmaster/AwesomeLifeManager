using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatusViewer : Scroll,Viewer
{
    ObjectPool theObjectPool;
    StatusManager theStatusManager;
    public void Awake()
    {
        theObjectPool = ObjectPool.instance;
        theStatusManager = StatusManager.instance;
    }

    void Viewer.GenBox()
    {
        int _i = 0;
        foreach(string k in theStatusManager.status.Keys)
        {
            if (theStatusManager.status[k].reveal)
            {
                GameObject t_box = theObjectPool.statusBoxQueue.Dequeue();
                t_box.SetActive(true);
                StatusBox t_status = t_box.GetComponent<StatusBox>();
                t_status.Setting(theStatusManager.status[k]);
                RectTransform t_rect = t_box.GetComponent<RectTransform>();
                t_rect.anchoredPosition = new Vector2(t_rect.anchoredPosition.x, -1 * t_rect.rect.height * _i);
                _i++;
            }
        }
        updateObjs<StatusBox>();
    }

    void Viewer.DeclareBox()
    {
        for(int i = 0; i < objs.Length; i++)
        {
            objs[i].gameObject.SetActive(false);
            theObjectPool.statusBoxQueue.Enqueue(objs[i].gameObject);
        }
        updateObjs<StatusBox>();
    }
    public override void onEndSwipe()
    {
        return;
    }

    public override void onStartSwipe()
    {
        return;
    }

    public MonoBehaviour GetObj()
    {
        return this;
    }
}
