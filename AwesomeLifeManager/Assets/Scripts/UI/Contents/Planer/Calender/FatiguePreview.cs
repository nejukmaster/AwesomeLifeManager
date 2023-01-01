using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FatiguePreview : MonoBehaviour
{
    public int fatigue;
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] Calender calender;

    private void Start()
    {
        fatigue = 100;
        Setting();
    }

    public void Setting()
    {
        int t_fat = 0;
        foreach(CalenderCell cell in calender.cells)
        {
            if(cell.insertedPlan != null)
                for(int i = 0; i < cell.insertedPlan.Count; i ++)
                    t_fat += cell.insertedPlan[i].costAP;
        }
        tmp.text = (fatigue-t_fat) + "/" + 100;
    }
}
