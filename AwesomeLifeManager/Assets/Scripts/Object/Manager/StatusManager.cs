using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data;
using System;

/*  이 클래스에서는 스테이터스를 담당해요. 
    스테이터를 만들고 등록할수 있어요. 
    스테이터스를 만들때 따로 조건등은 정할 수 없답니다. 
    단 스테이터스의 이름과 이를 나타낼 디스플레이 창을 지정할 순 있어요. 
    디스플레이는 비워두면 알아서 비공개 스테이터스로 정의된답니다.  */

public enum StatusType{
    Health,
    Ability,
    Tought,
    Sociality,
    Belief,
    Emotional,
    Etc
}
/*  이 클래스는 스테이터스가 어떤 구조로 만들어지는지 정의해요. 
    이름과 표시될 디스플레이등을 이 클래스에 담아서 등록하죠.   */
//스테이터스 클래스 선언
[System.Serializable]
public class Status : Variable{
    //스테이터스의 디스플레이
    public string name;
    public int value;
    public string description;
    public StatusType type;
    public bool reveal = true;
    public int[] initialRange = new int[2];
    public int[] valueRange = new int[2];
    public int talentLevel;

    public Status(string name, int value, string description, StatusType type, bool reveal, int[] initialRange, int[] valueRange)
    {
        this.name = name;
        this.value = value;
        this.description = description;
        this.type = type;
        this.reveal = reveal;
        this.initialRange = initialRange;
        this.talentLevel = 0;
        this.valueRange = valueRange;
    }

    public Status(string name, int value, string description, StatusType type)
    {
        this.name = name;
        this.value = value;
        this.description = description;
        this.type = type;
        this.reveal = true;
        talentLevel = 0;
    }
}

/*  스테이터스가 등록되고 관리될 매니져 클래스를 선언해요. 
    여기서는 스테이터스의 증감을 계산하거나, 
    등록된 스테이터스의 값을 다른 클래스가 참조할 수 있게 도와줘요. */
public class StatusManager : Manager
{
    public static StatusManager instance;
    //스테이터스 목록을 저장할 배열 생성
    public Dictionary<string,Status> status;

    PersonalityManager thePersonality;
    // Start is called before the first frame update
    void Start()
    {
        thePersonality = FindObjectOfType<PersonalityManager>();
        instance = this;
        List<Dictionary<string, object>> status_data = CSVReader.Read("DataSheet/Status");
        status = new Dictionary<string, Status>();
        for(int i = 0; i < status_data.Count; i++)
        {
            StatusType t = StatusType.Etc;
            switch(status_data[i]["type"]){
                case "Health":
                    t = StatusType.Health;
                    break;
                case "Ability":
                    t = StatusType.Ability;
                    break;
                case "Tought":
                    t = StatusType.Tought;
                    break;
                case "Sociality":
                    t = StatusType.Sociality;
                    break;
                case "Belief":
                    t = StatusType.Belief;
                    break;
                case "Emotional":
                    t = StatusType.Emotional;
                    break;
            }
            status[status_data[i]["code"].ToString()] = new Status(status_data[i]["name"].ToString(),
                        0, status_data[i]["description"].ToString(),
                        t,
                        (status_data[i]["is reveal"].ToString().Equals("o")),
                        new int[2] { Int32.Parse(status_data[i]["initial range"].ToString().Split("-")[0]), Int32.Parse(status_data[i]["initial range"].ToString().Split("-")[1]) },
                        new int[2] { Int32.Parse(status_data[i]["initial range"].ToString().Split("-")[0]), Int32.Parse(status_data[i]["initial range"].ToString().Split("-")[1]) });
        }
        Init();
    }

    //스테이터스 증가 함수
    public bool IncreaseStatus(string p_name, int p_num){
        foreach(string k in status.Keys)
            if(p_name == status[k].name ){
                if(status[k].value + p_num >= status[k].valueRange[0] && status[k].value + p_num <= status[k].valueRange[1]){
                    status[k].value += p_num;
                        return true;
                }
                else{
                    status[k].value = 0;
                    return false;
                }
            }
        return false;
    }

    //Status를 찾는 함수
    public Status GetStatus(string p_name){
        foreach(string k in status.Keys)
            if(status[k].name == p_name)
                return status[k];
        return null;
    }

    public override void Init()
    {
        foreach(Status s in status.Values)
        {
            s.value = UnityEngine.Random.Range(s.initialRange[0], s.initialRange[1] + 1);
        }
    }
}
