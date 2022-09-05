using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI : MonoBehaviour
{
    public UIManager theUIManager;

    public abstract bool onClickDown(Vector2 clickPos);

    public abstract bool onClickUp(float dragDis, Vector2 clickPos);

    public abstract bool onSwipe(Vector2 swipeStartp, Vector2 swipeEndp);

    void Start()
    {
        theUIManager = UIManager.instance;
    }
}
