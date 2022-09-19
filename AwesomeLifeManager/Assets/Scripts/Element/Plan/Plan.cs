using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plan
{
    public delegate bool rewardDel();
    public delegate bool conditionDel();

    public string name;
    public int costFatigue;
    public int costHungry;
    public rewardDel reward;
    public conditionDel condition;

    public Plan(string name, int costFatigue, int costHungry){
        this.name = name;
        this.costFatigue = costFatigue;
        this.costHungry = costHungry;
        this.reward = () => { return true; };
        this.condition = () => {return true;};
    }

    public Plan(string name, int costFatigue, int costHungry, rewardDel reward){
        this.name = name;
        this.costFatigue = costFatigue;
        this.costHungry = costHungry;
        this.reward = reward;
        this.condition = () => {return true;};
    }

    public Plan(string name, int costFatigue, int costHungry, conditionDel condition){
        this.name = name;
        this.costFatigue = costFatigue;
        this.costHungry = costHungry;
        this.reward = () => { return true; };
        this.condition = condition;
    }

    public Plan(string name, int costFatigue, int costHungry, rewardDel reward, conditionDel condition){
        this.name = name;
        this.costFatigue = costFatigue;
        this.costHungry = costHungry;
        this.reward = reward;
        this.condition = condition;
    }

    public bool Run()
    {
        return reward();
    }
}
