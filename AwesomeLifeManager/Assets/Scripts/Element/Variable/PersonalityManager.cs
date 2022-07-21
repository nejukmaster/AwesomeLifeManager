using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Personality : Variable{
    public string name;
    public bool slot;
    //변경할 스테이터스의 이름에 해당 변동식을 매핑
    public Dictionary<string, EquationDel> equaMap = new Dictionary<string,EquationDel>();

    //컨스트럭터 1
    public Personality(string name, Dictionary<string, EquationDel> equaMap, bool slot){
        this.name = name;
        this.equaMap = equaMap;
        this.slot = slot;
    }

    //각 스테이터스를 계산하여 반영할 함수 선언
    public void PersonalEquation(ref Status status){
        if(equaMap.ContainsKey(status.name)){
            status.buffs.Add(equaMap[status.name]);
        }
    }

    //해당 성격이 스테이터스를 변동시키는지 여부를 확인하는 함수 선언
    public bool HasEquation(){
        return equaMap.Count != 0;
    }
}
public class PersonalityManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI first_slot;
    [SerializeField] TextMeshProUGUI last_slot;

    public Dictionary<string, Personality> personalityMap = new Dictionary<string,Personality>();

    public Personality[] currentPersonality = new Personality[2];
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //성격 리스트를 파싱하여 Dictionary에 매핑할 함수 정의
    //이 부분에서 성격 등록을 처리
    void mapping(){
        personalityMap.Add("0_basic", new Personality("기본적인",new Dictionary<string,Variable.EquationDel>(){
            {"str",x=>x+1}, //변동식은 일반적인 함수로 작성해도 상관X, 하지만 코드가 난잡해질 가능성이 있으므로 왠만해선 람다식 사용
            {"hp",x=>x*2+1}
        }, false));
        personalityMap.Add("1_basic", new Personality("성격",new Dictionary<string,Variable.EquationDel>(){
            {"str",x=>x+4}, 
            {"hp",x=>x*0.3f+2f}
        }, true));
    }

    //성격을 변경하는 공용 함수 선언
    public void ChangePersonality(string p_code){
        Personality t_personal = personalityMap[p_code];
        if(!t_personal.slot){
            currentPersonality[0] = t_personal;
            first_slot.text = currentPersonality[0].name;
        }
        else{
            currentPersonality[1] = t_personal;
            last_slot.text = currentPersonality[1].name;
        }
        theStatus.Buff();
        theConviction.CheckCondition();
    }

    //현재 성격을 반환하는 공용 함수 선언
    public Personality[] GetPersonality(){
        return currentPersonality;
    }

    //성격조합 문자열을 받아오는 함수 생성
    public string GetCombinationStr(){
        string t_str = "";
        if(currentPersonality[0] != null)
            t_str += currentPersonality[0].name;
        t_str += " ";
        if(currentPersonality[1] != null)
            t_str += currentPersonality[1].name;
        return t_str;
    }

    public void Test1(){
        ChangePersonality("0_basic");
    }

    public void Test2(){
        ChangePersonality("1_basic");
    }
}
