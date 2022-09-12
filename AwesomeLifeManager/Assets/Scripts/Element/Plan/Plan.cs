using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plan
{
    public delegate void rewardDel();
    public delegate bool conditionDel();

    public string name;
    public int costFatigue;
    public int costHungry;
    public float costTime;
    public rewardDel reward;
    public conditionDel condition;

    public Plan(string name, int costFatigue, int costHungry, float costTime){
        this.name = name;
        this.costFatigue = costFatigue;
        this.costHungry = costHungry;
        this.costTime = costTime;
        this.reward = () => {};
        this.condition = () => {return true;};
    }

    public Plan(string name, int costFatigue, int costHungry, float costTime, rewardDel reward){
        this.name = name;
        this.costFatigue = costFatigue;
        this.costHungry = costHungry;
        this.costTime = costTime;
        this.reward = reward;
        this.condition = () => {return true;};
    }

    public Plan(string name, int costFatigue, int costHungry, float costTime, conditionDel condition){
        this.name = name;
        this.costFatigue = costFatigue;
        this.costHungry = costHungry;
        this.costTime = costTime;
        this.reward = () => {};
        this.condition = condition;
    }

    public Plan(string name, int costFatigue, int costHungry, float costTime, rewardDel reward, conditionDel condition){
        this.name = name;
        this.costFatigue = costFatigue;
        this.costHungry = costHungry;
        this.costTime = costTime;
        this.reward = reward;
        this.condition = condition;
    }
}
