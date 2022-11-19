using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PersonalityInfoPopup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;

    public void SetActive(bool p_bool, Personality p_personality)
    {
        this.gameObject.SetActive(p_bool);
        tmp.text = p_personality.description;
    }
}
