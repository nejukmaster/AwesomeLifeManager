using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI : MonoBehaviour
{
    public UIManager theUIManager;
    public bool active = true;
    public float clickedDelay;

    public abstract bool onClickDown(Vector2 clickPos);

    public abstract bool onClickUp(float dragDis, Vector2 clickPos);

    public abstract bool onSwipe(Vector2 swipeStartp, Vector2 swipeEndp);

    public virtual bool onDoubleClick(Vector2 clickPos, bool isActivate)
    {
        return false;
    }

    void Awake()
    {
        theUIManager = UIManager.instance;
    }

    public static void ToggleSubUI(GameObject p_obj, bool p_bool)
    {
        foreach(UI ui in p_obj.GetComponentsInChildren<UI>())
        {
            ui.active = p_bool;
        }
    }
}
