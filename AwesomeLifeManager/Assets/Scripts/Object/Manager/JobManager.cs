using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Job
{
    public string name;
    public float wage;
    public int jobAction;

    public Job(string name, float wage, int jobAction)
    {
        this.name = name;
        this.wage = wage;
        this.jobAction = jobAction;
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
        List<Dictionary<string, object>> timetable_data = CSVReader.Read("DataSheet/JobTimeTable");
        for(int i = 0; i < timetable_data.Count; i++)
        {
            foreach(string k in timetable_data[i].Keys)
                    Debug.Log(i.ToString()+""+k + ":"+timetable_data[i][k].ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
