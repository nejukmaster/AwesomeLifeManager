using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventItem
{
    public Event @event;

    public EventItem(Event @event)
    {
        this.@event = @event;
    }
}

public class EventManager : Manager
{
    public static EventManager instance;

    public Dictionary<string,Event> EventDic = new Dictionary<string,Event>();
    public List<EventItem> EventEnabled = new List<EventItem>();
    public EventItem StaticEvent;

    ChoiceManager theChoiceManager;
    TurnManager theTurnManager;

    void Awake()
    {
        instance = this;
        theChoiceManager = ChoiceManager.instance;
        theTurnManager = TurnManager.instance;
    }

    void RegisterEvent()
    {
        List<Dictionary<string, object>> event_data = CSVReader.Read("DataSheet/Event");
        for (int i = 0; i < event_data.Count; i ++)
        {
            Choice[] t_choices = new Choice[3] { theChoiceManager.choices[event_data[i]["choose1"].ToString()], null, null };
            if (theChoiceManager.choices.ContainsKey(event_data[i]["choose2"].ToString()))
            {
                t_choices[1] = theChoiceManager.choices[event_data[i]["choose2"].ToString()];
            }
            if (theChoiceManager.choices.ContainsKey(event_data[i]["choose3"].ToString()))
            {
                t_choices[2] = theChoiceManager.choices[event_data[i]["choose3"].ToString()];
            }
            Event e = new Event(event_data[i]["name"].ToString(), t_choices, event_data[i]["static"].ToString() == "o");
            EventDic.Add(event_data[i]["code"].ToString(),e);
        }

        EventDic["event_1"].condition = (date) =>
        {
            if (theTurnManager.currentTurnNum == 3 || theTurnManager.currentTurnNum == 74 || theTurnManager.currentTurnNum == 110)
            {
                return true;
            }
            return false;
        };
    }

    public void EnableEvent(string p_str)
    {
        if (!EventDic[p_str].isStatic)
            EventEnabled.Add(new EventItem(EventDic[p_str]));
        else
            StaticEvent = new EventItem(EventDic[p_str]);
    }

    public void CheckEvent(int p_date)
    {
        foreach(var pair in EventDic)
        {
            if (pair.Value.condition(p_date))
            {
                EventEnabled.Add(new EventItem(pair.Value));
            }
        }
    }

    public void InitEnabledEvent()
    {
        EventEnabled.Clear();
    }


    public override void Init()
    {
        InitEnabledEvent();
    }
}
