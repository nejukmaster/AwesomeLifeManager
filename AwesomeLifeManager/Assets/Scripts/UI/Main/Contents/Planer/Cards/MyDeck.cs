using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDeck : Manager
{
    public List<CardInform> cardInformList = new List<CardInform>();
    public Queue<ActionCard> cardQueue = new Queue<ActionCard>();

    public void AddCard(CardInform p_inform)
    {
        cardInformList.Add(p_inform);
    }

    public void GenerateDeck()
    {
        for(int i = 0; i < cardInformList.Count; i ++)
        {
            if (cardInformList[i].type != CardType.Event)
            {
                GameObject t_obj = ObjectPool.instance.actionCardQueue.Dequeue();
                t_obj.GetComponent<ActionCard>().inform = cardInformList[i];
                cardQueue.Enqueue(t_obj.GetComponent<ActionCard>());
            }
        }
    }

    public override void Init()
    {
        cardInformList = new List<CardInform>();
        cardQueue = new Queue<ActionCard>();
    }

    public void InitDeck()
    {
        cardQueue = new Queue<ActionCard>();
    }

    public void ShuffleDeck()
    {
        System.Random rand = new System.Random();
        Queue<ActionCard> t_queue = cardQueue;
        for (int i =0; i < 4; i++)
        {
            Queue<ActionCard> t_queue1 = new Queue<ActionCard>();
            Queue<ActionCard> t_queue2 = new Queue<ActionCard>();
            int r = rand.Next(0, t_queue.Count);
            for(int j = 0; j < r; j++)
            {
                t_queue1.Enqueue(t_queue.Dequeue());
            }
            for (int j = r; j < t_queue.Count; j++)
            {
                t_queue2.Enqueue(t_queue.Dequeue());
            }
            for(int j = 0; j < r; j++)
            {
                t_queue2.Enqueue(t_queue1.Dequeue());
            }
            t_queue = t_queue2;
        }
        cardQueue = t_queue;
    }
}
