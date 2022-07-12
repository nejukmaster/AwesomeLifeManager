using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent2 : Event
{
    Timer timer;
    StatusManager theStatus;

    public TestEvent2(string name, int priority) : base(name, priority){
        timer = Timer.instance;
        theStatus = GameObject.FindObjectOfType<StatusManager>();
    }

    public override bool ConditionFunc(){
        if(timer.time == 5 && theStatus.GetStatus("str").value > 20)
            return true;
        return false;
    }

    public override bool EventFunc(){
        Debug.Log(base.event_name);
        return true;
    }

    [RuntimeInitializeOnLoadMethod]
    public static void onStart(){
        new TestEvent1("test event 2", 0);
    }
}
