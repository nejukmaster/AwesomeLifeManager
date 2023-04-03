using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public string name;
    public bool enabled;

    public Buff(string name)
    {
        this.name = name;
    }
}
public class BuffManager : Manager
{
    public static BuffManager instance;

    public Dictionary<string, Buff> buffs = new Dictionary<string, Buff>();

    public override void Init()
    {
        foreach(Buff buff in buffs.Values)
        {
            buff.enabled = false;
        }
    }

    private void mapping()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        List<Dictionary<string, object>> buff_data = CSVReader.Read("DataSheet/Buffs");
        buffs = new Dictionary<string, Buff>();
        for (int i = 0; i < buff_data.Count; i++)
        {
            buffs[buff_data[i]["code"].ToString()] = new Buff(buff_data[i]["name"].ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
