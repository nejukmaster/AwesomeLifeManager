using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Turn
{
    public int turnNum;
    public List<Plan> settedPlan = new List<Plan>();
    TurnManager theTurnManager;
    EventManager theEventManager;
    bool eventFire = false;

    public Turn(int p_num)
    {
        this.turnNum = p_num;
        theTurnManager = TurnManager.instance;
        theEventManager = EventManager.instance;
    }

    public virtual void OnTurnStart(){

    }

    public virtual void OnTurnRun(){

    }

    public virtual void OnTurnEnd(){
        
    }

    public IEnumerator RunningCo(TurnProcessPopup t_popup, EventPopup e_popup)
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

            yield return new WaitForSeconds(2.0f);
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
            if (Random.Range(0, 100) < (int)(theEventManager.eventEncounterPercent * 100)) {
                if (theEventManager.EventEnabled.Count > 0)
                {
                    UI.ToggleSubUI(t_popup.gameObject, false);
                    e_popup.SetActive(true,theEventManager.GetRandomEvent(true));
                    e_popup.EventEncounter();
                    while (e_popup.gameObject.activeInHierarchy)
                    {
                        yield return new WaitForSeconds(2f);
                    }
                }
            }
            date++;
            //Event Fire
            yield return new WaitForSeconds(1.5f);
        }
        //TurnManager.instance.resultUI.SetActive(true);
        theTurnManager.currentTurnNum++;
        theTurnManager.currentTurn = new Turn(theTurnManager.currentTurnNum);
        theTurnManager.calender.InitialCells();
        t_popup.exitButton.gameObject.SetActive(true);
    }
}
