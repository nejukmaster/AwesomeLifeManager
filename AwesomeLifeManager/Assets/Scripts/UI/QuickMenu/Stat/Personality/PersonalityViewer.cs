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
        return;
    }

    void Viewer.GenBox()
    {
        return;
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
