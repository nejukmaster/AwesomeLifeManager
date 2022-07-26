using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*  시대상을 관리하는 클래스예요. 
    시대상 등록및 시작시 시대상 설정등을 담당해요.*/

/*  시대상의 정보를 담는 클래스예요. 
    시대상을 생성할땐 이름과 변경할 스테이터스와 변동식,
    조건과 우선도를 담을 수 있어요.   */
public class History : Variable{
    public string name;

    public int priority;

    //변경할 스테이터스의 이름에 해당 변동식을 매핑
    public Dictionary<string, EquationDel> equaMap = new Dictionary<string,EquationDel>();
    //확률조건이 들어갈  조건함수 대리자 배열
    public ConditionDel[] conditions = new ConditionDel[0];


    //컨스트럭터 1
    public History(string name, Dictionary<string, EquationDel> equaMap, int priority, params ConditionDel[] conditions){
        this.name = name;
        this.equaMap = equaMap;
        this.conditions = conditions;
        this.priority = priority;
    }

    //컨스트럭터 2
    public History(string name, int priority, params ConditionDel[] conditions){
        this.name = name;
        this.equaMap = new Dictionary<string,EquationDel>();
        this.priority = priority;
        this.conditions = conditions;
    }

    public bool equal(History other){
        return other.name == this.name;
    }

    //각 스테이터스를 계산하여 반영할 함수 선언
    public void HistoryEquation(ref Status status){
        if(equaMap.ContainsKey(status.name)){
            status.buffs.Add(equaMap[status.name]);
        }
    }

    //해당 성격이 스테이터스를 변동시키는지 여부를 확인하는 함수 선언
    public bool HasEquation(){
        return equaMap.Count != 0;
    }
}

/*  시대상을 게임에 적용할 시대상 매니져 클래스예요. 
    여기에선 시대상의 등록과 시대상 랜덤 설정을 담당하고, 
    다른 클래스에서 시대상을 참조할 수 있게 도와줘요.   */
public class HistoryManager : MonoBehaviour
{
    public Dictionary<string, History> historyMap = new Dictionary<string,History>();

    public History historicalBackground = null;

    int random_value = 0;

    [SerializeField] TextMeshProUGUI tmp;
    //StatusManager를 참조
    StatusManager theStatus;
    //ConvictionManager참조
    ConvictionManager theConviction;
    // Start is called before the first frame update
    void Start()
    {
        mapping();
        theStatus = FindObjectOfType<StatusManager>();
        theConviction = FindObjectOfType<ConvictionManager>();
        random_value = Utility.RandomValue();
        Debug.Log(random_value);

        SetRandomHistory();
    }

    //성격 리스트를 파싱하여 Dictionary에 매핑할 함수 정의
    //이 부분에서 성격 등록을 처리
    void mapping(){
        historyMap.Add("0_basic",new History("철기시대", new Dictionary<string, Variable.EquationDel>(){
            {"str",x=>x*1.2f},
            {"mp",x=>x+20f}
        },
        1,
        ()=>random_value<=30));
        historyMap.Add("1_basic",new History("중세유럽",
        2,
        ()=>random_value<=70));
        historyMap.Add("2_basic",new History("조선시대",
        3,
        ()=>random_value<=100));
    }

    public void SetHistory(string p_code){
        historicalBackground = historyMap[p_code];
        tmp.text = historyMap[p_code].name;
        theStatus.Buff();
    }

    public void SetHistory(History p_history){
        historicalBackground = p_history;
        tmp.text = p_history.name;
        theStatus.Buff();
    }

    public void SetRandomHistory(){
        List<History> t_history_params = new List<History>();
        foreach(var pair in historyMap){
            bool check = false;
            Variable.ConditionDel[] t_conditions = pair.Value.conditions;
            for(int i = 0; i < t_conditions.Length; i ++){
                if(t_conditions[i]())
                    check = true;
                else{
                    check = false;
                    break;
                }
            }
            if(check){
                t_history_params.Add(pair.Value);
            }
        }
        if(t_history_params.Count != 0)
            SetHistory(t_history_params[0]);
    }
}
