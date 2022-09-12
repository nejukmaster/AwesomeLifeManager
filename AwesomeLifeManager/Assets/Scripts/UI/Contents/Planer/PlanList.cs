using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanList : MonoBehaviour
{
    public PlanBox selected;
    public Plan selectedPlan;
    public List<GameObject> planListBoxList = new List<GameObject>();

    PlanManager thePlan;

    void Awake(){
        thePlan = PlanManager.instance;
    }
    
    public void SetActive(bool p_bool){
        this.gameObject.SetActive(p_bool);
        if(p_bool){
            foreach(var pair in thePlan.planDic){
                if(pair.Value.condition()){
                    planListBoxList.Add(PlanListBox.Generate(pair.Value, this));
                }
            }
            selected.gameObject.GetComponent<Button>().enabled = false;
        }
        else{
            for(int i = 0; i < planListBoxList.Count; i ++){
                planListBoxList[i].SetActive(false);
                ObjectPool.instance.planListBoxQueue.Enqueue(planListBoxList[i]);
            }
            planListBoxList.Clear();
            selected.contentPlan = selectedPlan;
            selected.tmp.text = selectedPlan.name;
            selected.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200);
            selected.gameObject.GetComponent<Button>().enabled = true;
        }
    }
}
