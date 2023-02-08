using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : Manager
{
    public static MoneyManager instance;

    public int money = 0;
    void Start(){
        instance = this;
    }
    public override void Init()
    {
        money = 0;
    }

    public bool AddMoney(int p_num)
    {
        if (money + p_num < 0)
            return false;
        money += p_num;
        return true;
    }
}
