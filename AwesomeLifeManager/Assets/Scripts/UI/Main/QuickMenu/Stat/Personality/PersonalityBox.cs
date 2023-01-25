using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PersonalityBox : MonoBehaviour
{
    PersonalityViewer personalityViewer;
    Personality personality;
    public void Setting(Personality t_per)
    {
        personality = t_per;
        this.GetComponentInChildren<TextMeshProUGUI>().text = t_per.name;
        personalityViewer = GetComponentInParent<PersonalityViewer>();
    }

    public void OnClick()
    {
        personalityViewer.personalityInfoPopup.SetActive(true,personality);
    }
}
