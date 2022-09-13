using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    public int turnNum;
    GameObject planWindow;
    public List<Plan> settedPlan = new List<Plan>();

    public Turn(int p_num)
    {
        this.turnNum = p_num;
    }

    public Turn(int p_num, GameObject planWindow)
    {
        this.planWindow = planWindow;
    }

    public void OnTurnStart(){

    }

    public void OnTurnRun(){

    }

    public void OnTurnEnd(){
        
    }

    public IEnumerator RunningCo()
    {
        IEnumerator<Plan> e1 = settedPlan.GetEnumerator();
        while(e1.MoveNext())
        {
            //???? ??? ??
            planWindow.SetActive(true);
            yield return new WaitForSeconds(5.0f);
            planWindow.SetActive(false);
            if (e1.Current.reward())
            {
                Debug.Log("Success!");
            }
            else
            {
                Debug.Log("Fail...");
            }
            //???? ??? ??
        }
    }
}
