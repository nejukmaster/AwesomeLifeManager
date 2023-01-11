using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plan
{
    public delegate bool rewardDel();
    public delegate bool conditionDel();

    public string name;
    public int costAP;
    public bool canDelete;
    public rewardDel reward;
    public conditionDel condition;

    public Plan(string name, int costAP){
        this.name = name;
        this.costAP = costAP;
        this.reward = () => { return true; };
        this.condition = () => {return true;};
        canDelete = true;
    }

    public Plan(string name, int costAP, bool canDelete)
    {
        this.name = name;
        this.costAP = costAP;
        this.reward = () => { return true; };
        this.condition = () => { return true; };
        this.canDelete = canDelete;
    }

#nullable enable
    public Plan(string name, int costAP, rewardDel? reward, conditionDel? condition, bool canDelete){
        this.name = name;
        this.costAP = costAP;
        if (reward != null)
            this.reward = reward;
        else
            this.reward = () => { return true; };
        if (condition != null)
            this.condition = condition;
        else
            this.condition = () => { return true; };
        this.canDelete = canDelete;
    }

    public bool Run()
    {
        return reward();
    }
}
