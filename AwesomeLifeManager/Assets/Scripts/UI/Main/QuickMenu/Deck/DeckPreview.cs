using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckPreview : MonoBehaviour
{
    [SerializeField] MyDeck myDeck;
    [SerializeField] TextMeshProUGUI tmp;

    public void UpdatePreview()
    {
        tmp.text = myDeck.cardInformList.Count + " / " + MyDeck.DECK_MAXIMUM;
    }
}
