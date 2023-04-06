using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public string name;
    public bool enabled;
    public List<string> personalities;

    public Buff(string name)
    {
        this.name = name;
        personalities = null;
    }
    public Buff(string name, List<string> personalities)
    {
        this.name = name;
        this.personalities = personalities;
    }
}
public class BuffManager : Manager
{
    public static BuffManager instance;

    public Dictionary<string, Buff> buffs = new Dictionary<string, Buff>();

    PersonalityManager thePersonalitymanager;

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
            if (buff_data[i]["condition"].ToString().Length >= 1) {
                buffs[buff_data[i]["code"].ToString()] = new Buff(buff_data[i]["name"].ToString(), new List<string>(buff_data[i]["condition"].ToString().Split(",")));
            }
            else
            {
                buffs[buff_data[i]["code"].ToString()] = new Buff(buff_data[i]["name"].ToString());
            }
        }
        thePersonalitymanager = PersonalityManager.instance;
    }

    public List<string>[] CheckBuff()
    {
        List<string>[] r_list = { new List<string>(), new List<string>() };
        foreach(var pair in buffs)
        {
            if(pair.Value.personalities != null)
            {
                bool t_bool = true;
                foreach(string s in pair.Value.personalities)
                {
                    if (thePersonalitymanager.personalityDic[s].enable == false)
                        break;
                }
                if (t_bool && !pair.Value.enabled)
                {
                    pair.Value.enabled = true;
                    r_list[0].Add(pair.Key);
                }
                else if(!t_bool && pair.Value.enabled){
                    pair.Value.enabled=false;
                    r_list[1].Add(pair.Key);
                }
            }
        }
        return r_list;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
