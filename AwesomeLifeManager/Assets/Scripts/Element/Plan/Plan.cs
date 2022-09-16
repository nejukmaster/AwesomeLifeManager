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
    public float costDay;
    public rewardDel reward;
    public conditionDel condition;

    public Plan(string name, int costFatigue, int costHungry, float costDay){
        this.name = name;
        this.costFatigue = costFatigue;
        this.costHungry = costHungry;
        this.costDay = costDay;
        this.reward = () => { return true; };
        this.condition = () => {return true;};
    }

    public Plan(string name, int costFatigue, int costHungry, float costDay, rewardDel reward){
        this.name = name;
        this.costFatigue = costFatigue;
        this.costHungry = costHungry;
        this.costDay = costDay;
        this.reward = reward;
        this.condition = () => {return true;};
    }

    public Plan(string name, int costFatigue, int costHungry, float costDay, conditionDel condition){
        this.name = name;
        this.costFatigue = costFatigue;
        this.costHungry = costHungry;
        this.costDay = costDay;
        this.reward = () => { return true; };
        this.condition = condition;
    }

    public Plan(string name, int costFatigue, int costHungry, float costDay, rewardDel reward, conditionDel condition){
        this.name = name;
        this.costFatigue = costFatigue;
        this.costHungry = costHungry;
        this.costDay = costDay;
        this.reward = reward;
        this.condition = condition;
    }

    public bool Run()
    {
        return reward();
    }
}
