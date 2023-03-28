using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json.Linq;
using System;
using UnityEditor.XR;

/*  이 클래스에서는 성격을 담당해요. 
    성격을 만들고 등록하는 일을 하죠. 또한 성격을 게임에 적용하는 일도 한답니다. 
    성격을 정할때는 마찬가지로 성격이 등장할 조건을 정의할 수 있어요. 
    성격은 가치관과 마찬가지로 여러개 보유가 가능하답니다.  */


/*  이 클래스는 성격이 어떤 구조로 만들어지는지 정의해요.
    조건과 이름등을 이 클래스에 담아서 등록하죠.    */
public class Personality : Variable{
    public string name;
    public string description;
    public PersonalityType type;
    List<string> conditions = new List<string>();

    //컨스트럭터 1
    public Personality(string name, string description, PersonalityType type, List<string> conditions){
        this.name = name;
        this.description = description;
        this.conditions = conditions;
        this.type = type;
    }

    public Personality(string name, PersonalityType type)
    {
        this.name = name;
        this.type = type;
        this.description = "";
    }

    public Personality(string name, string description, PersonalityType type)
    {
        this.name = name;
        this.type = type;
        this.description= description;
        Personality personality = this;
    }

    public bool CheckCondition()
    {
        bool r = false;
        foreach(string str in conditions)
        {
            string[] e = str.Split('|');
            int i1 = StatusManager.instance.status[e[0].Substring(1, e[0].Length-1)].value;
            int i2 = -1;
            for (int i = 0; i < e[1].Length; i ++)
            {
                if (e[1][i] == '\"')
                {
                    for(int j = i+1; j < e[1].Length; j++)
                    {
                        if (e[1][j] == '\"')
                        {
                            string s = e[1].Substring(i+1, j);
                            i = j + 1;
                            i2 = StatusManager.instance.status[s].value;
                            break;
                        }
                    }
                }
                if (e[1][i] == '+' || e[1][i] == '-' || e[1][i] == '*' || e[1][i] == '/')
                {
                    int n = 0;
                    Int32.TryParse(e[1].Substring(i + 1, e[1].Length).Trim(), out n);
                    switch (e[1][i])
                    {
                        case '+':
                            i2 = i2 + n;
                            break;
                        case '-':
                            i2 = i2 - n;
                            break;
                        case '*':
                            i2 = i2 * n;
                            break;
                        case '/':
                            i2 = i2 / n;
                            break;
                    }
                    break;
                }
            }
            if(i2 == -1)
            {
                Int32.TryParse(e[1], out i2);
            }
            switch (e[1])
            {
                case ">" :
                    if (i1 > i2) r = true;
                    else r = false;
                    break;
                case "<" :
                    if (i1 < i2) r = true;
                    else r = false;
                    break;
                case ">=":
                    if (i1 >= i2) r = true;
                    else r = false;
                    break;
                case "<=":
                    if (i1 <= i2) r = true;
                    else r = false;
                    break;
                case "==":
                    if (i1 == i2) r = true;
                    else r = false;
                    break;
            }
        }
        return r;
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
public class PersonalityManager : Manager
{
    public static PersonalityManager instance;

    public Dictionary<string, Personality> personalityDic = new Dictionary<string,Personality>();

    public List<Personality> personalities = new List<Personality>();
    //StatusManager를 참조
    StatusManager theStatus;
    //ConvictionManager참조
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        theStatus = FindObjectOfType<StatusManager>();
        List<Dictionary<string, object>> personality_data = CSVReader.Read("DataSheet/Personality");
        
        for (int i = 0; i < 3; i++)
        {
            List<string> t_list = new List<string>();
            string[] conditions = personality_data[i]["condition"].ToString().Split("/");
            foreach(string str in conditions)
            {
                t_list.Add(str);
            }
            personalityDic.Add(i.ToString("000"), new Personality(personality_data[i]["name"].ToString(), personality_data[i]["description"].ToString(), Utility.StringToEnum<PersonalityType>(personality_data[i]["classify"].ToString()),t_list));
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

    public void AddPersonality(Personality p_person)
    {
        personalities.Add(p_person);
    }

    public List<Personality> CheckPersonality(){
        List<Personality> r = new List<Personality>();
        foreach(var pair in personalityDic)
        {
            if (pair.Value.CheckCondition())
            {
                bool _r = false;
                for(int i = 0; i < personalities.Count; i++)
                {
                    if (personalities[i].equal(pair.Value)) _r = true;
                }
                if (_r) continue;
                r.Add(pair.Value);
            }
        }
        return r;
    }

    public override void Init()
    {
        personalities = new List<Personality>();
    }

    /*public void CheckCondition(){
        foreach(var pair in personalityDic){
            bool check = false;
            Variable.ConditionDel t_conditions = pair.Value.conditions;
            check = t_conditions();
            if(check && !CheckPersonality(new string[] {pair.Key})){
                AddPersonality(pair.Key);
            }
        }
    }*/
}
