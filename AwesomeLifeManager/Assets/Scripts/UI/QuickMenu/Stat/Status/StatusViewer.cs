using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusViewer : Scroll,Viewer
{
    ObjectPool theObjectPool;
    StatusManager theStatusManager;
    private void Start()
    {
        theObjectPool = ObjectPool.instance;
        theStatusManager = StatusManager.instance;
        this.GetComponent<ViewerContainer>().viewer[0] = this;
    }

    void Viewer.GenBox()
    {
        int _i = 0;
        for (int i = 0; i < theStatusManager.status.Length; i++)
        {
            if (theStatusManager.status[i].reveal)
            {
                GameObject t_box = theObjectPool.statusBoxQueue.Dequeue();
                t_box.SetActive(true);
                StatusBox t_status = t_box.GetComponent<StatusBox>();
                t_status.Setting(theStatusManager.status[_i]);
                RectTransform t_rect = t_box.GetComponent<RectTransform>();
                t_rect.anchoredPosition = new Vector2(t_rect.anchoredPosition.x, -1 * t_rect.rect.height * _i);
                _i++;
            }
        }
        updateObjs();
    }

    void Viewer.DeclareBox()
    {
        for(int i = 0; i < objs.Length; i++)
        {
            objs[i].gameObject.SetActive(false);
            theObjectPool.statusBoxQueue.Enqueue(objs[i].gameObject);
        }
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
