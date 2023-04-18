using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  이벤트를 관리하는 역할을 하는 매니져 클래스입니다. 
    여기서는 이벤트를 새로 만들거나 하는게 아니라 이미 만들어진 이벤트를 
    이곳에서 게임에 적용시켜 줘요.  */

public class EventItem
{
    public Event @event;
    public int weight;

    public EventItem(Event @event, int weight)
    {
        this.@event = @event;
        this.weight = weight;
    }

    public EventItem(Event @event)
    {
        this.@event = @event;
        this.weight = 1;
    }
}

public class EventManager : Manager
{
    public static EventManager instance;

    public Dictionary<string,Event> EventDic = new Dictionary<string,Event>();
    public List<EventItem> EventEnabled = new List<EventItem>();
    public EventItem StaticEvent;

    void Awake()
    {
        instance = this;
    }

    void RegisterEvent()
    {
        List<Dictionary<string, object>> event_data = CSVReader.Read("DataSheet/Event");
        for (int i = 0; i < event_data.Count; i ++)
        {
            Choice[] t_choices = new Choice[3] { null, null, null };
            Event e = new Event(event_data[i]["name"].ToString(),new Choice[3] { null,null,null}, 0);
            EventDic.Add(event_data[i]["code"].ToString(),e);
        }
        Debug.Log(event_data.Count);
    }

    public void EnableEvent(string p_str)
    {
        EventEnabled.Add(new EventItem(EventDic[p_str]));
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
