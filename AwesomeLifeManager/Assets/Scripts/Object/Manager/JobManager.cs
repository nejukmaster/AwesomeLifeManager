using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Job
{
    public string name;
    public float wage;
    List<int> jobEventsList;

    public Job(string name, float wage, List<int> jobEventsList)
    {
        this.name = name;
        this.wage = wage;
        this.jobEventsList = jobEventsList;
    }
}

public class JobBuff
{
    public int jobNum;

    public JobBuff(int jobNum)
    {
        this.jobNum = jobNum;
    }
}

public class JobItem
{
    public Job job;
    public int annual;

    public JobItem(Job job, int annual)
    {
        this.job = job;
        this.annual = annual;
    }   
}
public class JobManager : MonoBehaviour
{
    public static JobManager instance;

    public Dictionary<int, Job> jobDic = new Dictionary<int, Job>();
    public JobItem myJob;
    public JobBuff onBuff = null;

    EventManager theEventManager;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        theEventManager = EventManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
