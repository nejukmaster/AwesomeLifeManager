using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  이벤트를 관리하는 역할을 하는 매니져 클래스입니다. 
    여기서는 이벤트를 새로 만들거나 하는게 아니라 이미 만들어진 이벤트를 
    이곳에서 게임에 적용시켜 줘요.  */

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

    void Awake()
    {
        instance = this;
        theChoiceManager = ChoiceManager.instance;
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
    }

    public void EnableEvent(string p_str)
    {
        if (!EventDic[p_str].isStatic)
            EventEnabled.Add(new EventItem(EventDic[p_str]));
        else
            StaticEvent = new EventItem(EventDic[p_str]);
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
