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
        CardInform t_inform = new CardInform(name, type, des, illusteName, resultDes, new Action((cell,t_inform) => { ((CalenderCell)cell).InsertPlan(PlanManager.instance.planDic[(string)t_inform.dataQueue.Dequeue()]); }));
        t_inform.dataQueue.Enqueue(planCode.Clone());
        deck.AddCard(t_inform);
    }
}
