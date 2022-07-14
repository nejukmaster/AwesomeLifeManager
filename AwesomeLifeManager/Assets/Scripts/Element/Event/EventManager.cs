using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
