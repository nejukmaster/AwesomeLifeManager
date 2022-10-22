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

    public IEnumerator RunningCo(TurnProcessPopup t_popup)
    {
        IEnumerator<Plan> e1 = settedPlan.GetEnumerator();
        int date = 1;
        UI.ToggleSubUI(theTurnManager.MainUI, false);
        while(e1.MoveNext())
        {
            t_popup.planName.text = date.ToString();
            if (e1.Current != null)
            {
                t_popup.spriteAnimation.SetTrigger(e1.Current.name);
            }

            yield return new WaitForSeconds(5.0f);
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
            date++;
            //Event Fire
            yield return new WaitForSeconds(3.0f);
        }
        //TurnManager.instance.resultUI.SetActive(true);
        theTurnManager.currentTurnNum++;
        theTurnManager.currentTurn = new Turn(theTurnManager.currentTurnNum);
    }
}
