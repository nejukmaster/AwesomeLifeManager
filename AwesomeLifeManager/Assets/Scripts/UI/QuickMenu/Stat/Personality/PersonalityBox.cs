using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PersonalityBox : MonoBehaviour
{
    public void Setting(Personality t_per)
    {
        this.GetComponentInChildren<TextMeshProUGUI>().text = t_per.name;
    }
}
