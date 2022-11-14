using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*  이 클래스에서는 성격을 담당해요. 
    성격을 만들고 등록하는 일을 하죠. 또한 성격을 게임에 적용하는 일도 한답니다. 
    성격을 정할때는 마찬가지로 성격이 등장할 조건을 정의할 수 있어요. 
    성격은 가치관과 마찬가지로 여러개 보유가 가능하답니다.  */


/*  이 클래스는 성격이 어떤 구조로 만들어지는지 정의해요.
    조건과 이름등을 이 클래스에 담아서 등록하죠.    */
public class Personality : Variable{
    public string name;
    public string description;
    public ConditionDel conditions;
    public PersonalityType type;

    //컨스트럭터 1
    public Personality(string name, string description, PersonalityType type, ConditionDel conditionDel){
        this.name = name;
        this.description = description;
        this.conditions = conditionDel;
        this.type = type;
    }

    public Personality(string name, PersonalityType type)
    {
        this.name = name;
        this.type = type;
        this.description = "";
        this.conditions = () => { return false; };
    }

    public Personality(string name, string description, PersonalityType type)
    {
        this.name = name;
        this.type = type;
        this.description= description;
        this.conditions = () => { return false; };
    }

    public bool equal(Personality other){
        return other.name == this.name;
    }
}

public enum PersonalityType
{
    Humanity,
    Sociality,
    Ability,
    Emotionality,
    Mind,
    Vitality
}

/*  성격이 등록되고 관리될 매니져 클래스를 선언해요. 
    이곳에선 성격의 활성화/비활성화를 담당하고, 
    다른 클래스에서 성격의 활성/비활성 여부 및 
    성격의 조건 체크등을 확인할 수 있게 도와줘요.   */
public class PersonalityManager : MonoBehaviour
{
    public static PersonalityManager instance;

    public Dictionary<string, Personality> personalityDic = new Dictionary<string,Personality>();

    public List<Personality> personalities = new List<Personality>();
    //StatusManager를 참조
    StatusManager theStatus;
    //ConvictionManager참조
    ConvictionManager theConviction;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        theStatus = FindObjectOfType<StatusManager>();
        theConviction = FindObjectOfType<ConvictionManager>();
        List<Dictionary<string, object>> personality_data = CSVReader.Read("DataSheet/Personality");
        for (int i = 0; i < personality_data.Count; i++)
        {
            personalityDic.Add(i.ToString("000"), new Personality(personality_data[i]["name"].ToString(), "test", Utility.StringToEnum<PersonalityType>(personality_data[i]["classify"].ToString())));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //성격 리스트를 파싱하여 Dictionary에 매핑할 함수 정의
    //이 부분에서 성격 등록을 처리
    void mapping(){

    }

    bool contains_ignore_cases(List<Personality> list, Personality other){
        bool r = false;
        foreach(Personality p in list){
            if(p.equal(other)){
                r = true;
                break;
            }
        }
        return r;
    }

    public void AddPersonality(string p_code){
        personalities.Add(personalityDic[p_code]);
    }

    public bool CheckPersonality(string[] p_code_list){
        for(int i = 0; i < p_code_list.Length; i ++){
            if(!contains_ignore_cases(personalities,personalityDic[p_code_list[i]]))
                return false;
        }
        return true;
    }

    public void CheckCondition(){
        foreach(var pair in personalityDic){
            bool check = false;
            Variable.ConditionDel t_conditions = pair.Value.conditions;
            check = t_conditions();
            if(check && !CheckPersonality(new string[] {pair.Key})){
                AddPersonality(pair.Key);
            }
        }
    }
}
