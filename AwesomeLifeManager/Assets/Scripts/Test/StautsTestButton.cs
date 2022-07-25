using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  스테이터스를 상승시키는 버튼의 코드예요.
    스테이터스 테스트할라고 만들었어요.    */
public class StautsTestButton : MonoBehaviour
{
    StatusManager theStatus;
    public string increase_status_name;
    public int increase_status_much;
    // Start is called before the first frame update
    void Start()
    {
        theStatus = FindObjectOfType<StatusManager>();
    }

    public void onClick(){
        theStatus.IncreaseStatus(increase_status_name, increase_status_much);
    }
}
