using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
