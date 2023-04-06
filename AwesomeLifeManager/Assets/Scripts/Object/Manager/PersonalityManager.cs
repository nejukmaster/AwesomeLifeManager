using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json.Linq;
using System;
using UnityEditor.XR;

public class Personality : Variable{
    public string name;
    public string description;
    public PersonalityType type;
    List<string> conditions = new List<string>();
    public List<string> cards = new List<string> ();
    public bool enable;

    public Personality(string name, string description, PersonalityType type, List<string> conditions){
        this.name = name;
        this.description = description;
        this.conditions = conditions;
        this.type = type;
    }
    public Personality(string name, string description, PersonalityType type, List<string> conditions, List<string> cards)
    {
        this.name = name;
        this.description = description;
        this.conditions = conditions;
        this.type = type;
        this.cards = cards;
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

    public void Enable()
    {
        foreach(string s in cards)
        {
            MyDeck.instance.AddCard(CardManager.instance.cardInformList[s]);
        }
    }

    public void Disable()
    {
        foreach (string s in cards)
        {
            MyDeck.instance.cardInformList.Remove(CardManager.instance.cardInformList[s]);
        }
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

public class PersonalityManager : Manager
{
    public static PersonalityManager instance;

    public Dictionary<string, Personality> personalityDic = new Dictionary<string,Personality>();
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
            personalityDic.Add(personality_data[i]["code"].ToString(), new Personality(personality_data[i]["name"].ToString(),
                                                                                        personality_data[i]["description"].ToString(),
                                                                                        Utility.StringToEnum<PersonalityType>(personality_data[i]["classify"].ToString()),
                                                                                         new List<string>(personality_data[i]["condition"].ToString().Split("/")),
                                                                                        new List<string>(personality_data[i]["card"].ToString().Split("/"))));
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

    public List<string>[] CheckPersonality(){
        List<string>[] r = { new List<string>(), new List<string>() };
        foreach(var pair in personalityDic)
        {
            if (pair.Value.CheckCondition())
            {
                if (!pair.Value.enable)
                {
                    r[0].Add(pair.Key);
                    pair.Value.Enable();
                }

            }
            else
            {
                if (pair.Value.enable)
                {
                    r[1].Add(pair.Key);
                    pair.Value.Disable();
                }
            }
        }
        return r;
    }

    public override void Init()
    {
        foreach(var p in personalityDic)
        {
            p.Value.Disable();
        }
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
