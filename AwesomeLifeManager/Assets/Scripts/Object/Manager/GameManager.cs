using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Manager[] managers;
    public bool[,] timeTable = new bool[12,28];

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        List<Dictionary<string, object>> timetable_data = CSVReader.Read("DataSheet/JobTimeTable");
        for (int i = 0; i < timetable_data.Count; i++)
        {
            foreach (string k in timetable_data[i].Keys)
            {
                switch (k)
                {
                    case "Jan.":
                        timeTable[0, i] = !(timetable_data[i][k].ToString() == "");
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

        }
    }
}
