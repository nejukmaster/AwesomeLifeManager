using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Job
{
    public string name;
    public float wage;
    public string jobPlan;

    public Job(string name, float wage, string jobPlan)
    {
        this.name = name;
        this.wage = wage;
        this.jobPlan = jobPlan;
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
    public bool[,] timeTable = new bool[12, 28];

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
            {
                switch (k)
                {
                    case "Jan.":
                        timeTable[0,i] = !(timetable_data[i][k].ToString() == "");
                        break;
                    case "Fab.":
                        timeTable[1, i] = !(timetable_data[i][k].ToString() == "");
                        break;
                    case "Mar.":
                        timeTable[2, i] = !(timetable_data[i][k].ToString() == "");
                        break;
                    case "Apr.":
                        timeTable[3, i] = !(timetable_data[i][k].ToString() == "");
                        break;
                    case "May.":
                        timeTable[4, i] = !(timetable_data[i][k].ToString() == "");
                        break;
                    case "Jun.":
                        timeTable[5, i] = !(timetable_data[i][k].ToString() == "");
                        break;
                    case "Jul.":
                        timeTable[6, i] = !(timetable_data[i][k].ToString() == "");
                        break;
                    case "Aug.":
                        timeTable[7, i] = !(timetable_data[i][k].ToString() == "");
                        break;
                    case "Sep.":
                        timeTable[8, i] = !(timetable_data[i][k].ToString() == "");
                        break;
                    case "Oct.":
                        timeTable[9, i] = !(timetable_data[i][k].ToString() == "");
                        break;
                    case "Nov.":
                        timeTable[10, i] = !(timetable_data[i][k].ToString() == "");
                        break;
                    case "Dec.":
                        timeTable[11, i] = !(timetable_data[i][k].ToString() == "");
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
