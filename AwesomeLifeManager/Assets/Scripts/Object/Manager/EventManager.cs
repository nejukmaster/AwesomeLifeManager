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

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public Dictionary<int,Event> EventDic = new Dictionary<int,Event>();
    public List<EventItem> EventEnabled = new List<EventItem>();
    public float eventEncounterPercent;

    void Awake()
    {
        instance = this;
        for(int i = 0; i < 5; i++)
        {
            EventEnabled.Add(new EventItem(new Event("Test Event"+i,
                                new Choice[] { new Choice("00", "test choice first!"), new Choice("01", "test choice second!"), new Choice("02", "test choice third") })));
        }
    }

    void RegisterEvent()
    {
        List<Dictionary<string, object>> event_data = CSVReader.Read("DataSheet/Event");
        for (int i = 0; i < event_data.Count; i ++)
        {
            Event e = new Event(event_data[i]["name"].ToString(),new Choice[3] { null,null,null});
            EventDic.Add(i,e);
        }
    }

    public void CheckEvent()
    {

    }

    public Event GetRandomEvent(bool p_bool)
    {
        List<EventItem> t_eventList = new List<EventItem>();
        int t_sum = 0;
        foreach(EventItem i in EventEnabled)
        {
            t_sum += i.weight;
            for(int j = 0; j < i.weight; j++)
            {
                t_eventList.Add(new EventItem(i.@event, EventEnabled.IndexOf(i)));
            }
        }
        EventItem r_event = t_eventList[Random.Range(0, t_sum)];
        if (p_bool)
        {
            EventEnabled.RemoveAt(r_event.weight);
        }
        return r_event.@event;
    }
}
