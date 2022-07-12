using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event
{
    public delegate bool EventDelegate();

    public string event_name;
    public int event_priority;
    public EventDelegate conditionFunc;
    public EventDelegate eventFunc;

    public Event(string name, int priority){
        event_name = name;
        event_priority = priority;
        conditionFunc = new EventDelegate(ConditionFunc);
        eventFunc = new EventDelegate(EventFunc);
        EventManager.instance.Events.Add(this); 
    }
    
    public abstract bool ConditionFunc();
    public abstract bool EventFunc();

    public static Event FindTopPriorityEvent(List<Event> events){
        List<Event> top_priority = new List<Event>();
        foreach(Event e in events){
            if(top_priority.Count == 0)
                top_priority.Add(e);
            else if(top_priority[0].event_priority > e.event_priority){
                top_priority.Clear();
                top_priority.Add(e);
            }
            else if(top_priority[0].event_priority == e.event_priority)
                top_priority.Add(e);
        }
        top_priority = Utility.Shuffle<Event>(top_priority);
        return top_priority[0];
    }
}
