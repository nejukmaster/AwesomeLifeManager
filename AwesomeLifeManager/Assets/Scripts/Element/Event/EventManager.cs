using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  이벤트를 관리하는 역할을 하는 매니져 클래스입니다. 
    여기서는 이벤트를 새로 만들거나 하는게 아니라 이미 만들어진 이벤트를 
    이곳에서 게임에 적용시켜 줘요.  */

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public EventWindow window;

    public List<Event> Events = new List<Event>();
    public int eventsNum;

    void Awake()
    {
        instance = this;
    }

    public void CheckEvent(){
        eventsNum = Events.Count;
        List<Event> t_events = new List<Event>();
        foreach(Event e in Events)
            if(e.conditionFunc()){
                t_events.Add(e);
            }
        if(t_events.Count > 0)
            Event.FindTopPriorityEvent(t_events).eventFunc();
    }
}
