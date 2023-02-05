using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardToDeckButton : MonoBehaviour
{
    [SerializeField] MyDeck deck;
    public string name;
    public CardType type;
    public string des;
    public string illusteName;
    public string resultDes;
    public string planCode;
    public int cost;
    public void OnClick()
    {
        if (type == CardType.Action)
        {
            deck.AddCard(CardManager.instance.cardInformList[0]);
        }
        else if(type == CardType.Event)
        {
            CardInform t_inform = new CardInform(name, type, des, illusteName, resultDes, new Action((eventManager, t_inform) => {
                                                                                                                                 ((EventManager)eventManager).EventEnabled.Add(new EventItem((new Event(name,
                                                                                                                                    new Choice[] { new Choice("00", "test choice first!"), new Choice("01", "test choice second!"), new Choice("02", "test choice third") },0)))); }),cost);
            deck.AddCard(t_inform);
        }
    }
}
