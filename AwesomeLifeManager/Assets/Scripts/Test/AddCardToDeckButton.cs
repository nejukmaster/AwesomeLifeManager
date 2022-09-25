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
    public void OnClick()
    {
        deck.AddCard(new CardInform(name, type, des, illusteName, resultDes,new Action( (cell) => { ((CalenderCell)cell).InsertPlan(PlanManager.instance.planDic[planCode]); })));
    }
}
