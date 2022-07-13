using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    //타이머 인스턴스 선언
    public static Timer instance;

    EventManager theEvent;

    public double currentTime;
    public int time = 0;
    public bool increase_timer = true;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        theEvent = FindObjectOfType<EventManager>();
        theEvent.CheckEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if(increase_timer){
            currentTime += Time.deltaTime;
            if(currentTime >= 10d){
                time ++;
                currentTime -= 10d;
                //10초당 한번 이벤트 체크
                theEvent.CheckEvent();
            }
        }
    }
}
