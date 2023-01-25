using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AP : Point
{
    [SerializeField] Image apImg;
    [SerializeField] int max_ap;
    [SerializeField] int current_ap;

    public void SetMax(int p_max){
        max_ap = p_max;
    }

    public void Setap(int p_value){
        current_ap = p_value;
    }

    public int GetMax(){
        return max_ap;
    }

    public int Getap(int p_value){
        return current_ap;
    }

    public override void SetParam(){
        apImg.fillAmount = (float)current_ap/(float)max_ap;
    }
}
