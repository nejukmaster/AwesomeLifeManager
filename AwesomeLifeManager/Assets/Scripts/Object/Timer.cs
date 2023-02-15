using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  10초에 카운트 1을 하는 일반적인 타이머예요. 
    리소스관리를 위해 왠만하면 여기서만 Update()문을 다룰거예요.    */
public class Timer : MonoBehaviour
{

    //타이머 인스턴스 선언
    public static Timer instance;
    EventManager theEvent;
    CharacterUIManager theCharacterUI;

    public double currentTime;
    public int time = 0;
    public bool increase_timer = true;
    public bool date = true;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        theEvent = FindObjectOfType<EventManager>();
        theCharacterUI = FindObjectOfType<CharacterUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(increase_timer){
            currentTime += Time.deltaTime;
            theCharacterUI.SetParam();
        }
    }
}
