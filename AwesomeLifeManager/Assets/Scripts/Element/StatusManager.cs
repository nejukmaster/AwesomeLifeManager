using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//스테이터스 클래스 선언
//[System.Serializable]로 선언된 클래스는 유니티 인스펙터창에서 수정 가능
[System.Serializable]
public class Status{
    //스테이터스의 디스플레이
    public TextMeshProUGUI tmp = null;
    public string name;
    public int value;
    public int value_buffed = -1;

    public int GetValue(){
        if(value_buffed >= 0)
            return value_buffed;
        else
            return value;
    }
}
public class StatusManager : MonoBehaviour
{
    //스테이터스 목록을 저장할 배열 생성
    //[SerializeField]로 선언된 변수는 유니티 인스펙터창에서 수정 가능
    [SerializeField] Status[] status;

    Timer timer;
    ConvictionManager theConviction;
    // Start is called before the first frame update
    void Start()
    {
        FillStatusBlank();
        timer = Timer.instance;
        theConviction = FindObjectOfType<ConvictionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < status.Length; i ++){
            ConvictionManager.Conviction conviction = theConviction.GetConviction();
            if(conviction.HasEquation()){
                conviction.ConvictionEquation(ref status[i]);
                FillStatusBlank();
            }
        }
    }

    void FillStatusBlank(){
        for(int i = 0; i < status.Length; i++){
            //스테이터스 디스플레이 널체크
            if(status[i].tmp != null){
                status[i].tmp.text = status[i].name + " : " + status[i].GetValue();
            }
        }
    }

    //스테이터스 증가 함수
    public void IncreaseStatus(string p_name, int p_num){
        for(int i = 0; i < status.Length; i++)
            if(p_name == status[i].name){
                status[i].value += p_num;
                if(status[i].tmp != null)
                    status[i].tmp.text = status[i].name +  " : " + status[i].GetValue();
            }
    }

    //Status를 찾는 함수
    public Status GetStatus(string p_name){
        for(int i = 0; i < status.Length; i++)
            if(status[i].name == p_name)
                return status[i];
        return null;
    }
}
