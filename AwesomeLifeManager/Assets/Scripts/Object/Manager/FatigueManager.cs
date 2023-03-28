using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatigueManager : Manager
{
    public static FatigueManager instance;

    public int MaxFatigue = 100;
    public int Fatigue;

    StatusManager theStatusManager;

    public override void Init()
    {
        MaxFatigue = 100 + theStatusManager.status["sta_health"].value;
        Fatigue = MaxFatigue;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        theStatusManager = StatusManager.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
