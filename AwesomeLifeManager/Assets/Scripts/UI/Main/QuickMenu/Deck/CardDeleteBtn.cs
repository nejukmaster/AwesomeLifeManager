using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeleteBtn : MonoBehaviour
{
    public CardViewer openedViewer = null;

    [SerializeField] MyDeck myDeck;

    public void OnClick()
    {
        CardIcon[] t_icons = openedViewer.GetComponentsInChildren<CardIcon>();
        foreach(CardIcon i in t_icons)
        {
            if (i.gameObject.activeInHierarchy && i.isCheck)
            {
                myDeck.cardInformList.Remove(i.cardInform);
            }
        }
        openedViewer.deactivateSelectionMode();
        openedViewer.DeleteIcons();
        openedViewer.GenIcons();
    }
}
