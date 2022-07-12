using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public static Timer instance;

    EventManager theEvent;

    public double currentTime;
    public int time;
    public bool increase_timer = true;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        theEvent = FindObjectOfType<EventManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(increase_timer){
            currentTime += Time.deltaTime;
            if(currentTime >= 10d){
                time ++;
                currentTime -= 10d;
                theEvent.CheckEvent();
            }
        }
    }
}
