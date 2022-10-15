using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanBox : MonoBehaviour
{
    public int planNum;

    public void Check()
    {
        Calender calender = GameObject.FindObjectOfType<Calender>();
        calender.Check(this, GetComponentInChildren<Toggle>().isOn);
    }
}
