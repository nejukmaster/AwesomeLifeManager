using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Status{
    public TextMeshProUGUI tmp = null;
    public string name;
    public int value;

}
public class StatusManager : MonoBehaviour
{
    [SerializeField] Status[] status;

    Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < status.Length; i++){
            if(status[i].tmp != null){
                status[i].tmp.text = status[i].name+" : "+status[i].value;
            }
        }

        timer = Timer.instance;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timer.time == 5 && GetStatus("str").value > 20);
    }

    public void IncreaseStatus(string p_name, int p_num){
        for(int i = 0; i < status.Length; i++)
            if(p_name == status[i].name){
                status[i].value += p_num;
                if(status[i].tmp != null)
                    status[i].tmp.text = status[i].name+" : "+status[i].value;
            }
    }

    public Status GetStatus(string p_name){
        for(int i = 0; i < status.Length; i++)
            if(status[i].name == p_name)
                return status[i];
        return null;
    }
}
