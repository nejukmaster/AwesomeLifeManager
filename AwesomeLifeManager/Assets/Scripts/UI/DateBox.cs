using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DateBox : MonoBehaviour
{
    TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateText(bool p_bool){
        if(p_bool) tmp.text = "낮"; else tmp.text = "밤";
    }
}
