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
        planDic.Add("01", new Plan("재능 찾기",5, () => { turnProcessPopup.AddLog("도덕성 +10"); theStatusManager.IncreaseStatus("도덕성", 10); turnProcessPopup.AddLog("친화력 +20"); theStatusManager.IncreaseStatus("친화력", 20); return true; }, null,true));
        planDic.Add("02", new Plan("basic02",10, () => { turnProcessPopup.AddLog("외향성 +50"); theStatusManager.IncreaseStatus("외향성", 51); return true; }, null,false));
    }

    public override void Init()
    {

    }
}
