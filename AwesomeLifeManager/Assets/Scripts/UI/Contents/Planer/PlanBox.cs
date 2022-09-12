using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanBox : MonoBehaviour
{
    [SerializeField] PlanList planList;
    [SerializeField] public TextMeshProUGUI tmp;

    public Plan contentPlan;
    
    public void Click(){
        planList.selected = this;
        planList.SetActive(true);
    }
}
