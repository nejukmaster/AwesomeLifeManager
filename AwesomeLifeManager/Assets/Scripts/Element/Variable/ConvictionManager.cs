using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*  이 클래스에서는 가치관을 담당해요. 
    가치관을 제작하고 등록하는 기능과, 가치관을 게임에 적용하는 기능을 해요. 
    가치관을 등록할때는 마찬가지로 이 가치관이 등장할 조건을 정의할 수 있어요.  */


/*  실제 가치관의 구조를 선언한 클래스예요. 
    가치관의 이름과 등장할 조건등을 담고 있죠. 
    이 클래스에 이들을 담아서 매니져에 등록해요.    */
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

    public bool equal(Conviction other){
        return other.name == this.name;
    }

}

/*  가치관의 등록과 활성 비활성을 담당할 매니져에요. 
    이 클래스는 또한, 다른 클래스가 특정 가치관 활성 여부및 
    가치관의 등장 조건 체크등을 할 수 있도록 도와줘요.  */
public class ConvictionManager : MonoBehaviour
{
    //StatusManager를 참조
    StatusManager theStatus;
    //PersonalityManager 참조
    PersonalityManager thePersonality;
    public List<Conviction> convictions = new List<Conviction>();
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //가치관 리스트를 파싱하여 Dictionary에 매핑할 함수 정의
    //이 부분에서 가치관 등록을 처리
    void mapping(){
        convictionMap.Add("test", new Conviction("테스트",
         ()=>theStatus.GetStatus("int").value > 11,
         ()=>thePersonality.CheckPersonality(new string[]{"0_basic","1_basic"})));
    }

    bool contains_ignore_cases(List<Conviction> list, Conviction other){
        bool r = false;
        foreach(Conviction p in list){
            if(p.equal(other)){
                r = true;
                break;
            }
        }
        return r;
    }

    public void AddConviction(string p_code){
        convictions.Add(convictionMap[p_code]);
        ConvictionBox.Generate(convictionMap[p_code].name);
    }

    public bool CheckConviction(string[] p_code_list){
        for(int i = 0; i < p_code_list.Length; i ++){
            if(!contains_ignore_cases(convictions,convictionMap[p_code_list[i]]))
                return false;
        }
        return true;
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
            if(check && !CheckConviction(new string[]{pair.Key}))
                AddConviction(pair.Key);
        }
    }
}
