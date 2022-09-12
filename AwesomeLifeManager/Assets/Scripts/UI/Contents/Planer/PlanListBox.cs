using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanListBox : MonoBehaviour
{
    static int pibot = 0;
    [SerializeField] TextMeshProUGUI tmp;
    Plan contentPlan;
    PlanList planList;
    
    public static GameObject Generate(Plan p_plan, PlanList p_list){
        GameObject t_box = ObjectPool.instance.planListBoxQueue.Dequeue();
        RectTransform rect = t_box.GetComponent<RectTransform>();
        t_box.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,-1 * rect.rect.height * pibot);
        t_box.GetComponent<PlanListBox>().tmp.text = p_plan.name;
        t_box.GetComponent<PlanListBox>().planList = p_list;
        t_box.GetComponent<PlanListBox>().contentPlan = p_plan;
        t_box.SetActive(true);
        pibot++;
        return t_box;
    }

    public void Click(){
        planList.selectedPlan = contentPlan;
        planList.SetActive(false);
    }
}
