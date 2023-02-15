using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimeHandler : UI
{
    [SerializeField] EventPopup eventPopup;

    public override bool onClickDown(Vector2 clickPos)
    {
        return false;
    }

    public override bool onClickUp(float dragDis, Vector2 clickPos)
    {
        if (eventPopup.eventEncounterAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            eventPopup.Container.SetActive(true);
            this.gameObject.SetActive(false);
            return true;
        }
        else
            return false;
    }

    public override bool onSwipe(Vector2 swipeStartp, Vector2 swipeEndp)
    {
        return false;
    }
}
