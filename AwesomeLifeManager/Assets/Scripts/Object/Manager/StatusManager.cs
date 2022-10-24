using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data;

/*  이 클래스에서는 스테이터스를 담당해요. 
    스테이터를 만들고 등록할수 있어요. 
    스테이터스를 만들때 따로 조건등은 정할 수 없답니다. 
    단 스테이터스의 이름과 이를 나타낼 디스플레이 창을 지정할 순 있어요. 
    디스플레이는 비워두면 알아서 비공개 스테이터스로 정의된답니다.  */


/*  이 클래스는 스테이터스가 어떤 구조로 만들어지는지 정의해요. 
    이름과 표시될 디스플레이등을 이 클래스에 담아서 등록하죠.   */
//스테이터스 클래스 선언
[System.Serializable]
public class Status : Variable{
    //스테이터스의 디스플레이
    public string name;
    public int value;
    public string description;
    public bool reveal = true;

    public Status(string name, int value, string description, bool reveal)
    {
        this.name = name;
        this.value = value;
        this.description = description;
        this.reveal = reveal;
    }

    public Status(string name, int value, string description)
    {
        this.name = name;
        this.value = value;
        this.description = description;
        this.reveal = true;
    }
}

/*  스테이터스가 등록되고 관리될 매니져 클래스를 선언해요. 
    여기서는 스테이터스의 증감을 계산하거나, 
    등록된 스테이터스의 값을 다른 클래스가 참조할 수 있게 도와줘요. */
public class StatusManager : MonoBehaviour
{
    public static StatusManager instance;
    //스테이터스 목록을 저장할 배열 생성
    public Status[] status;

    ConvictionManager theConviction;
    PersonalityManager thePersonality;
    // Start is called before the first frame update
    void Start()
    {
        theConviction = FindObjectOfType<ConvictionManager>();
        thePersonality = FindObjectOfType<PersonalityManager>();
        instance = this;
        List<Dictionary<string, object>> status_data = CSVReader.Read("DataSheet/Status");
        status = new Status[status_data.Count];
        for(int i = 0; i < status_data.Count; i++)
        {
            status[i] = new Status(status_data[i]["name"].ToString(), 0, status_data[i]["description"].ToString(), (status_data[i]["is reveal"].ToString().Equals("o")));
        }
    }

    //스테이터스 증가 함수
    public void IncreaseStatus(string p_name, int p_num){
        for(int i = 0; i < status.Length; i++)
            if(p_name == status[i].name){
                status[i].value += p_num;
            }
        //theConviction.CheckCondition();
        //thePersonality.CheckCondition();
    }

    //Status를 찾는 함수
    public Status GetStatus(string p_name){
        for(int i = 0; i < status.Length; i++)
            if(status[i].name == p_name)
                return status[i];
        return null;
    }
}
