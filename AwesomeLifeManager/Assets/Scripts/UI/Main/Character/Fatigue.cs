using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Fatigue : Point
{
    [SerializeField] Image fatigueImg;
    [SerializeField] int max_fatigue;
    [SerializeField] int current_fatigue;

    public void SetMax(int p_max){
        max_fatigue = p_max;
    }

    public void SetFatigue(int p_value){
        current_fatigue = p_value;
    }

    public int GetMax(){
        return max_fatigue;
    }

    public int GetFatigue(int p_value){
        return current_fatigue;
    }

    public override void SetParam(){
        fatigueImg.fillAmount = (float)current_fatigue/(float)max_fatigue;
    }
}
