using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Manager[] managers;
    public int[,] timeTable = new int[12,28];

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        List<Dictionary<string, object>> timetable_data = CSVReader.Read("DataSheet/TimeTable");
        for (int i = 0; i < timetable_data.Count; i++)
        {
            foreach (string k in timetable_data[i].Keys)
            {
                int v = (BitConverter.GetBytes(timetable_data[i][k].ToString() == "V")[0] << 1) | (BitConverter.GetBytes(timetable_data[i][k].ToString() == "A")[0]);
                switch (k)
                {
                    case "Jan.":
                        timeTable[0, i] = v;
                        break;
                    case "Fab.":
                        timeTable[1, i] = v;
                        break;
                    case "Mar.":
                        timeTable[2, i] = v;
                        break;
                    case "Apr.":
                        timeTable[3, i] = v;
                        break;
                    case "May.":
                        timeTable[4, i] = v;
                        break;
                    case "Jun.":
                        timeTable[5, i] = v;
                        break;
                    case "Jul.":
                        timeTable[6, i] = v;
                        break;
                    case "Aug.":
                        timeTable[7, i] = v;
                        break;
                    case "Sep.":
                        timeTable[8, i] = v;
                        break;
                    case "Oct.":
                        timeTable[9, i] = v;
                        break;
                    case "Nov.":
                        timeTable[10, i] = v;
                        break;
                    case "Dec.":
                        timeTable[11, i] = v;
                        break;
                }
            }
        }
        for(int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 28; j++)
            {
                Debug.Log(timeTable[i, j]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitGame()
    {
        try
        {
            foreach (Manager m in managers)
            {
                m.Init();
            }
        }
        catch (Exception err)
        {
            Debug.Log(err);
        }
    }
}
