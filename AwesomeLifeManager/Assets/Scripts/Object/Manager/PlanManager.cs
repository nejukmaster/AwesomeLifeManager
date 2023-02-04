using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanManager : Manager
{
    public static PlanManager instance;
    
    public Dictionary<string,Plan> planDic = new Dictionary<string,Plan>();

    [SerializeField] TurnProcessPopup turnProcessPopup;

    StatusManager theStatusManager;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        theStatusManager = StatusManager.instance;
        mapping();
    }

    void mapping(){
        planDic.Add("00", new Plan("basic00", 10, () => { turnProcessPopup.AddLog("도덕성 +81"); theStatusManager.IncreaseStatus("도덕성", 81); turnProcessPopup.AddLog("명성 +41"); theStatusManager.IncreaseStatus("명성", 41); turnProcessPopup.AddLog("외향성 +51"); theStatusManager.IncreaseStatus("외향성", 51); return true; }, null,true));
        planDic.Add("Job_00", new Plan("직업행동", 0, () => { return true; }, null,false));
        planDic.Add("Action_01", new Plan("재능 찾기",5, () => {
            List<int> t_status = new List<int>();
            for(int i = 0; i < theStatusManager.status.Length; i ++){
                if(theStatusManager.status[i].type == StatusType.Ability){
                    t_status.Add(i);
                }
            }
            theStatusManager.status[t_status[Random.Range(0,t_status.Count)]].value += theStatusManager.status[t_status[Random.Range(0,t_status.Count)]].value * (Random.Range(10,16)/100);
            return true; 
        }, null,true));
        planDic.Add("02", new Plan("basic02",10, () => { turnProcessPopup.AddLog("외향성 +50"); theStatusManager.IncreaseStatus("외향성", 51); return true; }, null,false));
    }

    public override void Init()
    {

    }
}
