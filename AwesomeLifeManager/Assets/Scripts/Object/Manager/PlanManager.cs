using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanManager : Manager
{
    public static PlanManager instance;
    
    public Dictionary<string,Plan> planDic = new Dictionary<string,Plan>();

    [SerializeField] TurnProcessPopup turnProcessPopup;

    StatusManager theStatusManager;
    JobManager theJobManager;
    MoneyManager theMoneyManager;
    TurnManager theTurnManager;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        theStatusManager = StatusManager.instance;
        theJobManager = JobManager.instance;
        theMoneyManager = MoneyManager.instance;
        theTurnManager = TurnManager.instance;
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
            string t_stName = theStatusManager.status[t_status[Random.Range(0,t_status.Count)]].name;
            int d_status = Utility.Step(1,10,(int)theStatusManager.GetStatus(t_stName).value * (Random.Range(10,16)/100));
            theStatusManager.IncreaseStatus(t_stName,d_status);
            turnProcessPopup.AddLog(t_stName+" +"+d_status);
            return true; 
        }, null,true));
        planDic.Add("Action_02", new Plan("일과 업무",10, () => {
            int t_jobPro = 2 + (int)(theStatusManager.GetStatus("성실성").value/theStatusManager.GetStatus("집중력").value*0.7f);
            theStatusManager.IncreaseStatus("직업 숙련도",t_jobPro);
            theStatusManager.IncreaseStatus("스트레스",3);
            turnProcessPopup.AddLog("스트레스 +3");
            turnProcessPopup.AddLog("직업 숙련도 +"+t_jobPro);
            return true; 
        }, null,true));
        planDic.Add("Action_03", new Plan("야근",10, () => {
            int t_jobPro = 3 + (int)(theStatusManager.GetStatus("성실성").value/theStatusManager.GetStatus("집중력").value*0.7f);
            theStatusManager.IncreaseStatus("직업 숙련도",t_jobPro);
            theStatusManager.IncreaseStatus("스트레스",5);
            turnProcessPopup.AddLog("스트레스 +5");
            turnProcessPopup.AddLog("직업 숙련도 +"+t_jobPro);
            return true; 
        }, null,true));
        planDic.Add("Action_04", new Plan("해외 출장",10, () => {
            int t_jobPro = 3 + (int)(theStatusManager.GetStatus("성실성").value/theStatusManager.GetStatus("화술").value*0.7f);
            theStatusManager.IncreaseStatus("직업 숙련도",t_jobPro);
            theStatusManager.IncreaseStatus("화술",5);
            theStatusManager.IncreaseStatus("스트레스",-3);
            turnProcessPopup.AddLog("스트레스 -3");
            turnProcessPopup.AddLog("화술 +5");
            turnProcessPopup.AddLog("직업 숙련도 +"+t_jobPro);
            return true; 
        }, null,true));
        planDic.Add("Action_05", new Plan("퇴사",5, () => {
            theStatusManager.GetStatus("직업 숙련도").value = 0;
            theStatusManager.IncreaseStatus("건강",10);
            theStatusManager.IncreaseStatus("스트레스",-10);
            turnProcessPopup.AddLog("스트레스 -10");
            turnProcessPopup.AddLog("건강 +10");
            turnProcessPopup.AddLog("직업 숙련도 초기화");
            theJobManager.InitJob(0);
            turnProcessPopup.AddLog("\"무직\" 획득");
            return true; 
        }, null,true));
        planDic.Add("Action_06", new Plan("퇴사",5, () => {
            theStatusManager.IncreaseStatus("성실성",2);
            theStatusManager.IncreaseStatus("계획성",3);
            theStatusManager.IncreaseStatus("재력",4);
            turnProcessPopup.AddLog("성실성 +2");
            turnProcessPopup.AddLog("계획성 +3");
            turnProcessPopup.AddLog("재력 +4");
            return true; 
        }, null,true));
        planDic.Add("Action_07", new Plan("퇴사",5, () => {
            theStatusManager.IncreaseStatus("성실성",3);
            theStatusManager.IncreaseStatus("계획성",4);
            theStatusManager.IncreaseStatus("재력",5);
            turnProcessPopup.AddLog("성실성 +3");
            turnProcessPopup.AddLog("계획성 +4");
            turnProcessPopup.AddLog("재력 +5");
            return true; 
        }, null,true));
        planDic.Add("Action_08", new Plan("식재료 구매",5, () => {
            int costMoney = (int)(theStatusManager.GetStatus("재력").value*100*0.7f);
            if(theMoneyManager.money < costMoney){
                //행동 실패 이벤트 발생
                turnProcessPopup.AddLog("돈이 부족합니다.");
            }
            else{
               theMoneyManager.AddMoney(-1 * costMoney);
               theStatusManager.IncreaseStatus("재력",-1);
               theTurnManager.actionCool["식재료 구매"].cool = 14;
               turnProcessPopup.AddLog("재력 -1");
               turnProcessPopup.AddLog("돈을 "+costMoney+"만큼 잃었습니다.");
                turnProcessPopup.AddLog("식재료를 구매했습니다!");
            }
            return true; 
        }, null,true));
        planDic.Add("Action_09", new Plan("건강 검진",5, () => {
            if(theMoneyManager.money < 100000){
                //행동 실패 이벤트 발생
                turnProcessPopup.AddLog("돈이 부족합니다.");
            }
            else{
               theMoneyManager.AddMoney(-100000);
               theStatusManager.IncreaseStatus("계획성",3);
               theStatusManager.IncreaseStatus("건강",6);
               turnProcessPopup.AddLog("계획성 +3");
               turnProcessPopup.AddLog("계획성 +6");
               turnProcessPopup.AddLog("돈을 100000만큼 잃었습니다.");
                turnProcessPopup.AddLog("건강검진을 받았습니다!");
            }
            return true; 
        }, null,true));
        planDic.Add("02", new Plan("basic02",10, () => { turnProcessPopup.AddLog("외향성 +50"); theStatusManager.IncreaseStatus("외향성", 51); return true; }, null,false));
    }

    public override void Init()
    {

    }
}
