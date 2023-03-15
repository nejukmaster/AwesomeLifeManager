using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Turn
{
    public int turnNum;
    public List<Plan> settedPlan = new List<Plan>();
    TurnManager theTurnManager;
    EventManager theEventManager;
    PersonalityManager thePersonalityManager;
    bool eventFire = false;

    public Turn(int p_num)
    {
        this.turnNum = p_num;
    }

    public virtual void OnTurnStart(){

    }

    public virtual void OnTurnRun(){

    }

    public virtual void OnTurnEnd(){
        
    }

    public IEnumerator RunningCo(TurnProcessPopup t_popup, EventPopup e_popup)
    {
        if (theTurnManager == null || theEventManager == null || thePersonalityManager == null)
        {
            theTurnManager = TurnManager.instance;
            theEventManager = EventManager.instance;
            thePersonalityManager = PersonalityManager.instance;
        }
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
                t_popup.AddLog("Free Act.");
                for(int i = 0; i < 5; i++)
                {
                    theEventManager.EventEnabled.Add(new EventItem(new Event("Test Event"+i,
                                new Choice[] { new Choice("00", "test choice first!"), new Choice("01", "test choice second!"), new Choice("02", "test choice third") }, 0)));
                }
            }
            foreach (string s in theTurnManager.actionCool.Keys)
            {

                if (theTurnManager.actionCool[s].cool > 0)
                {
                    theTurnManager.actionCool[s].cool--;
                    if(theTurnManager.actionCool[s].cool == 0)
                    {
                        if(s == "식재료 구매")
                        {
                            theTurnManager.actionCool[s].action.actionDel(theEventManager, null);
                        }
                    }
                }
            }
            for (int i = 0; i < theEventManager.EventEnabled.Count; i ++)
            {
                UI.ToggleSubUI(t_popup.gameObject, false);
                e_popup.SetActive(true, theEventManager.EventEnabled[i].@event);
                e_popup.EventEncounter();
                while (e_popup.gameObject.activeInHierarchy)
                {
                    yield return new WaitForSeconds(2f);
                }
            }
            List<Personality> t_list = thePersonalityManager.CheckPersonality();
            for(int i = 0; i < t_list.Count; i ++)
            {
                thePersonalityManager.AddPersonality(t_list[i]);
                t_popup.AddLog(t_list[i].name + "(이)가 활성화 되었습니다.");
            }
            theEventManager.InitEnabledEvent();
            date++;
            //Event Fire
            yield return new WaitForSeconds(1.5f);
        }
        //TurnManager.instance.resultUI.SetActive(true);
        theTurnManager.calender.hand.InitHand();
        theTurnManager.calender.hand.myDeck.InitDeck();
        theTurnManager.calender.hand.myDeck.GenerateDeck();
        theTurnManager.currentTurnNum++;
        theTurnManager.currentTurn = new Turn(theTurnManager.currentTurnNum);
        theTurnManager.calender.cellsSetted = false;
        t_popup.exitButton.gameObject.SetActive(true);
    }
}
