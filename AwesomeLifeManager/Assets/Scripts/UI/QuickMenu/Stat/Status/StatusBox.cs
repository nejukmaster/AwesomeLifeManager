using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusBox : MonoBehaviour
{
    public void Setting(Status p_status)
    {
        TextMeshProUGUI[] tmps = this.GetComponentsInChildren<TextMeshProUGUI>();
        tmps[0].text = p_status.name;
        tmps[1].text = p_status.value.ToString();
        tmps[2].text = p_status.description;
    }
}
