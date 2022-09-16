using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    public int turnNum;
    public List<Plan> settedPlan = new List<Plan>();

    public Turn(int p_num)
    {
        this.turnNum = p_num;
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
            //Event Fire
            TurnManager.instance.planWindow.SetActive(true);
            yield return new WaitForSeconds(5.0f);
            TurnManager.instance.planWindow.SetActive(false);
            if (e1.Current.reward())
            {
                Debug.Log(e1.Current.name + " Success!");
            }
            else
            {
                Debug.Log("Fail...");
            }
            //Event Fire
            yield return new WaitForSeconds(3.0f);
        }
        TurnManager.instance.resultUI.SetActive(true);
    }
}
