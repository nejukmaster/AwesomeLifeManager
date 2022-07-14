using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : Event
{
    Timer timer;
    //컨스트럭터
    public TestEvent(string name, int priority) : base(name, priority){
    }

    //ConditionFunc재정의
    public override bool ConditionFunc(){
        if(timer == null) timer = Timer.instance;
        if(timer.time == 3)
            return true;
        return false;
    }

    //EventFunc재정의
    public override bool EventFunc(){
        EventManager.instance.window.Up("Test Event fired!");
        return true;
    }

    //실행시 스스로를 생성
    //[RuntimeInitializeOnLoadMethod]는 Awake함수가 호출된후 자동으로 해당 함수를 호출한다.
    [RuntimeInitializeOnLoadMethod]
    public static void onStart(){
        new TestEvent("test event", 2);
    }
}
