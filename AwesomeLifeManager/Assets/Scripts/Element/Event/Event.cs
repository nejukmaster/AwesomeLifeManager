using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  이벤트의 기본 골자를 담은 클래스예요. 
    이제 이걸 상속해서 변형해서 이벤트를 새로 만듭니다.
    만들때 이벤트가 일어난 결과 및 이벤트가 일어날 조건등을 설정하여 만들어요. 
    이후 자동으로 전체 이벤트 목록에 자동으로 등록합니다.   */
    
//추상클래스 Event
public abstract class Event
{
    //대리자 선언
    public delegate bool EventDelegate();

    //이벤트 이름
    public string event_name;
    //이벤트의 우선순위
    public int event_priority;
    //조건 함수를 담는 대리자 선언
    public EventDelegate conditionFunc;
    //이벤트의 작동 함수를 담을 대리자 선언
    public EventDelegate eventFunc;

    //컨스트럭터
    public Event(string name, int priority){
        event_name = name;
        event_priority = priority;
        conditionFunc = new EventDelegate(ConditionFunc);
        eventFunc = new EventDelegate(EventFunc);
        EventManager.instance.Events.Add(this); 
    }
    
    //조건 함수로 쓸 추상메소드 선언
    public abstract bool ConditionFunc();
    //이벤트의 작동 함수로 사용할 추상메소드 선언
    public abstract bool EventFunc();

    //이벤트 리스트에서 최우선순위 이벤트를 찾아주는 함수 생성
    //우선순위가 같을 경우 랜덤으로 선택
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
