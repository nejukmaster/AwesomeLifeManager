using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Conviction : Variable{

    public string name;
    //해금 조건 함수의 대리자들을 저장하는 리스트 생성
    public ConditionDel[] conditions = new ConditionDel[0];
    //컨스트럭터 1
    public Conviction(string name, params ConditionDel[] conditions){
        this.name = name;
        this.conditions = conditions;
    }
    //컨스트럭터2
    public Conviction(string name){
        this.name = name;
    }
}

public class ConvictionManager : MonoBehaviour
{
    //StatusManager를 참조
    StatusManager theStatus;
    //PersonalityManager 참조
    PersonalityManager thePersonality;
    //기본 가치관
    public Conviction conviction;
    TextMeshProUGUI tmp = null;
    //가치관 코드와 그에 대응하는 표시형을 담는 Dictionary
    public Dictionary<string, Conviction> convictionMap = new Dictionary<string,Conviction>();

    // Start is called before the first frame update
    void Start()
    {
        mapping();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        theStatus = FindObjectOfType<StatusManager>();
        thePersonality = FindObjectOfType<PersonalityManager>();
        ChangeConviction("None");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //가치관 리스트를 파싱하여 Dictionary에 매핑할 함수 정의
    //이 부분에서 가치관 등록을 처리
    void mapping(){
        convictionMap.Add("None", new Conviction("없음"));
        convictionMap.Add("test", new Conviction("테스트",
         ()=>theStatus.GetStatus("int").value > 11? true : false,
         ()=>thePersonality.GetCombinationStr() == "기본적인 성격"));
    }

    //현재 가치관을 반환하는 공용 함수 선언
    public Conviction GetConviction(){
        return conviction;
    }

    //가치관을 변경하는 공용 함수 선언
    public void ChangeConviction(string p_code){
        conviction = convictionMap[p_code];
        tmp.text = conviction.name;
    }

    public void ChangeConviction(Conviction p_convic){
        conviction = p_convic;
        tmp.text = conviction.name;
    }

    public void CheckCondition(){
        foreach(var pair in convictionMap){
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
            if(check)
                ChangeConviction(pair.Value);
        }
    }
}
