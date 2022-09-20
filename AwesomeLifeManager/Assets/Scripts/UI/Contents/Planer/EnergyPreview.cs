using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class EnergyPreview : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fatiguePrev;
    [SerializeField] TextMeshProUGUI apPrev;
    CharacterManager theCharacterManager;

    void Awake()
    {
        theCharacterManager = CharacterManager.instance;
    }

    public void SetPreview(int p_fatigue, int p_ap)
    {
        fatiguePrev.text = (theCharacterManager.fatigue.GetMax()-p_fatigue) + "/" + theCharacterManager.fatigue.GetMax();
        apPrev.text = (theCharacterManager.ap.GetMax()-p_ap) + "/" + theCharacterManager.ap.GetMax();
    }
}
