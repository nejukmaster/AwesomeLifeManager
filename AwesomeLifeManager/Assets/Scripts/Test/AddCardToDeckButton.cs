using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardToDeckButton : MonoBehaviour
{
    [SerializeField] MyDeck deck;
    [SerializeField] CardInform cardInform;
    public void OnClick()
    {
        deck.AddCard(cardInform);
    }
}
