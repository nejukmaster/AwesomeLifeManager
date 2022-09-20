using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plan
{
    public delegate bool rewardDel();
    public delegate bool conditionDel();

    public string name;
    public int costFatigue;
    public int costAP;
    public rewardDel reward;
    public conditionDel condition;

    public Plan(string name, int costFatigue, int costAP){
        this.name = name;
        this.costFatigue = costFatigue;
        this.costAP = costAP;
        this.reward = () => { return true; };
        this.condition = () => {return true;};
    }

    public Plan(string name, int costFatigue, int costAP, rewardDel reward){
        this.name = name;
        this.costFatigue = costFatigue;
        this.costAP = costAP;
        this.reward = reward;
        this.condition = () => {return true;};
    }

    public Plan(string name, int costFatigue, int costAP, conditionDel condition){
        this.name = name;
        this.costFatigue = costFatigue;
        this.costAP = costAP;
        this.reward = () => { return true; };
        this.condition = condition;
    }

    public Plan(string name, int costFatigue, int costAP, rewardDel reward, conditionDel condition){
        this.name = name;
        this.costFatigue = costFatigue;
        this.costAP = costAP;
        this.reward = reward;
        this.condition = condition;
    }

    public bool Run()
    {
        return reward();
    }
}
