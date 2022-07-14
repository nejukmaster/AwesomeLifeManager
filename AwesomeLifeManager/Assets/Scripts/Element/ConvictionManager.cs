using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConvictionManager : MonoBehaviour
{
    //가치관의 스테이터스 변동식을 저장할 대리자 생성
    public delegate int EquationDel(int x);
    //StatusManager를 참조
    StatusManager theStatus;

    //가치관을 나타낼 클래스 Conviction선언
    public class Conviction{

        public string name;
        //변경할 스테이터스의 이름에 해당 변동식을 매핑
        public Dictionary<string, EquationDel> equaMap = new Dictionary<string,EquationDel>();

        //컨스트럭터 1
        public Conviction(string name, Dictionary<string, EquationDel> equaMap){
            this.name = name;
            this.equaMap = equaMap;
        }
        //컨스트럭터2
        public Conviction(string name){
            this.name = name;
        }
        
        //각 스테이터스를 계산하여 반영할 함수 선언
        public void ConvictionEquation(ref Status status){
            if(equaMap.ContainsKey(status.name)){
                status.value_buffed = equaMap[status.name](status.value);
            }
        }
        //해당 가치관이 스테이터스를 변동시키는지 여부를 확인하는 함수 선언
        public bool HasEquation(){
            return equaMap.Count != 0;
        }
    }

    //기본 가치관
    public string conviction = "None";
    TextMeshProUGUI tmp = null;
    //가치관 코드와 그에 대응하는 표시형을 담는 Dictionary
    public Dictionary<string, Conviction> convictionMap = new Dictionary<string,Conviction>();

    // Start is called before the first frame update
    void Start()
    {
        mapping();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        theStatus = FindObjectOfType<StatusManager>();
        tmp.text = convictionMap[conviction].name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //가치관 리스트를 파싱하여 Dictionary에 매핑할 함수 정의
    //이 부분에서 가치관 등록을 처리
    void mapping(){
        convictionMap.Add("None", new Conviction("없음"));
        convictionMap.Add("test", new Conviction("테스트",new Dictionary<string, EquationDel>(){
            {"str",x=>x+1}, //변동식은 일반적인 함수로 작성해도 상관X, 하지만 코드가 난잡해질 가능성이 있으므로 왠만해선 람다식 사용
            {"hp",x=>x*2+1}
        }));
    }

    //현재 가치관을 반환하는 공용 함수 선언
    public Conviction GetConviction(){
        return convictionMap[conviction];
    }

    //가치관을 변경하는 공용 함수 선언
    public void ChangeConviction(string p_code){
        conviction = p_code;
        tmp.text = convictionMap[conviction].name;
    }

    public void Test(){
        ChangeConviction("test");
    }
    
}
