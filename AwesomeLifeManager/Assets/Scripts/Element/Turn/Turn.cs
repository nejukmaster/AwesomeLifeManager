using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Turn
{
    public int turnNum;
    public List<Plan> settedPlan = new List<Plan>();
    TurnManager theTurnManager;

    public Turn(int p_num)
    {
        this.turnNum = p_num;
        theTurnManager = TurnManager.instance;
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
            theTurnManager.planWindow.SetActive(true);
            if (e1.Current != null)
            {
                theTurnManager.planWindow.GetComponentInChildren<TextMeshProUGUI>().text = e1.Current.name;
            }
            else
                theTurnManager.planWindow.GetComponentInChildren<TextMeshProUGUI>().text = "Free Acting...";
            yield return new WaitForSeconds(5.0f);
            theTurnManager.planWindow.SetActive(false);
            if (e1.Current != null )
            {
                if (e1.Current.reward())
                    Debug.Log(e1.Current.name + " Success!");
                else
                    Debug.Log("Fail...");
            }
            else
            {
                Debug.Log("Free Act.");
            }
            //Event Fire
            yield return new WaitForSeconds(3.0f);
        }
        //TurnManager.instance.resultUI.SetActive(true);
    }
}
