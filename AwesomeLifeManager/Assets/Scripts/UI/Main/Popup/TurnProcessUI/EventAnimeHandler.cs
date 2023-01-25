using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimeHandler : UI
{
    [SerializeField] EventPopup eventPopup;

    bool isEnd =false;

    public override bool onClickDown(Vector2 clickPos)
    {
        return false;
    }

    public override bool onClickUp(float dragDis, Vector2 clickPos)
    {
        if (isEnd)
        {
            eventPopup.Container.SetActive(true);
            this.gameObject.SetActive(false);
            return true;
        }
        else
            return false;
    }

    public void OnEndEncounterAnime()
    {
            isEnd = true;
    }

    public override bool onSwipe(Vector2 swipeStartp, Vector2 swipeEndp)
    {
        return false;
    }
}
