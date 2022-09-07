using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Hungry : Point
{
    [SerializeField] Image hungryImg;
    [SerializeField] int max_hungry;
    [SerializeField] int current_hungry;

    public void SetMax(int p_max){
        max_hungry = p_max;
    }

    public void SetHungry(int p_value){
        current_hungry = p_value;
    }

    public int GetMax(){
        return max_hungry;
    }

    public int GetHungry(int p_value){
        return current_hungry;
    }

    public override void SetParam(){
        hungryImg.fillAmount = (float)current_hungry/(float)max_hungry;
    }
}
