using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : Event
{
    Timer timer;

    public TestEvent(string name, int priority) : base(name, priority){
        timer = Timer.instance;
    }

    public override bool ConditionFunc(){
        if(Timer.instance.time == 3)
            return true;
        return false;
    }

    public override bool EventFunc(){
        Debug.Log(base.event_name);
        return true;
    }

    [RuntimeInitializeOnLoadMethod]
    public static void onStart(){
        new TestEvent("test event", 2);
    }
}
