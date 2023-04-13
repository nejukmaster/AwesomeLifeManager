using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plan
{
    public delegate bool rewardDel(Plan p);
    public delegate bool conditionDel();

    public string name;
    public bool canDelete;
    public rewardDel reward;
    public conditionDel condition;
    public int NoR = 0;

    public Plan(string name){
        this.name = name;
        this.reward = (p) => { return true; };
        this.condition = () => {return true;};
        canDelete = true;
    }

    public Plan(string name, bool canDelete)
    {
        this.name = name;
        this.reward = (p) => { return true; };
        this.condition = () => { return true; };
        this.canDelete = canDelete;
    }

#nullable enable
    public Plan(string name, rewardDel? reward, conditionDel? condition, bool canDelete){
        this.name = name;
        if (reward != null)
            this.reward = reward;
        else
            this.reward = (p) => { return true; };
        if (condition != null)
            this.condition = condition;
        else
            this.condition = () => { return true; };
        this.canDelete = canDelete;
    }

    public bool Run()
    {
        NoR += 1;
        return reward(this);
    }
}
