using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent1 : Event
{
    public TestEvent1(string name, int priority) : base(name, priority){

    }

    public override bool ConditionFunc(){
        if(Timer.instance.time == 5)
            return true;
        return false;
    }

    public override bool EventFunc(){
        EventManager.instance.window.Up("Test Event 1 fired!");
        return true;
    }

    [RuntimeInitializeOnLoadMethod]
    public static void onStart(){
        new TestEvent1("test event 1", 1);
    }
}
