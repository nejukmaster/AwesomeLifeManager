using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanManager : MonoBehaviour
{
    public static PlanManager instance;
    
    public Dictionary<string,Plan> planDic = new Dictionary<string,Plan>();
    public List<Plan> settedPlan = new List<Plan>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        mapping();
    }

    void mapping(){
        planDic.Add("00", new Plan("basic00",10,10,2.5f));
        planDic.Add("01", new Plan("basic01",10,10,8f));
        planDic.Add("02", new Plan("basic02",10,10,3f));
    }
}
