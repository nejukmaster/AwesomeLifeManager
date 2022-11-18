using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanManager : MonoBehaviour
{
    public static PlanManager instance;
    
    public Dictionary<string,Plan> planDic = new Dictionary<string,Plan>();

    [SerializeField] TurnProcessPopup turnProcessPopup;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        mapping();
    }

    void mapping(){
        planDic.Add("00", new Plan("basic00", 10, () => { turnProcessPopup.AddLog("basic00 success"); return true; }, null));
        planDic.Add("01", new Plan("basic01",10));
        planDic.Add("02", new Plan("basic02",10));
    }
}
