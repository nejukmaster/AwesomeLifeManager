using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent2 : Event
{
    StatusManager theStatus;

    public TestEvent2(string name, int priority) : base(name, priority){
        theStatus = GameObject.FindObjectOfType<StatusManager>();
    }

    public override bool ConditionFunc(){
        if(Timer.instance.time == 5 && theStatus.GetStatus("str").value > 20)
            return true;
        return false;
    }

    public override bool EventFunc(){
        EventManager.instance.window.Up("Test Event 2 fired!");
        return true;
    }

    [RuntimeInitializeOnLoadMethod]
    public static void onStart(){
        new TestEvent2("test event 2", 0);
    }
}
