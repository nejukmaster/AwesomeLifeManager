using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  이벤트를 관리하는 역할을 하는 매니져 클래스입니다. 
    여기서는 이벤트를 새로 만들거나 하는게 아니라 이미 만들어진 이벤트를 
    이곳에서 게임에 적용시켜 줘요.  */

public class EventItem
{
    public int number;
    public Event @event;
    public string num;

    public EventItem()
    {

    }
}

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public List<EventItem> EventDic = new List<EventItem>();
    public int eventsNum;

    void Awake()
    {
        instance = this;
    }

    void RegisterEvent()
    {
        List<Dictionary<string, object>> event_data = CSVReader.Read("DataSheet/Event");
        for (int i = 0; i < event_data.Count; i ++)
        {
            Event e = new Event(event_data[i]["name"].ToString(),new Choice[3] { null,null,null});
        }
    }

    public void CheckEvent(){

    }
}
