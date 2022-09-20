using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class EnergyPreview : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fatiguePrev;
    [SerializeField] TextMeshProUGUI apPrev;
    CharacterUIManager theCharacterUIManager;

    public void Awake()
    {
        theCharacterUIManager = CharacterUIManager.instance;
    }

    public void SetPreview(int p_fatigue, int p_ap)
    {
        Debug.Log(theCharacterUIManager);
        fatiguePrev.text = (theCharacterUIManager.fatigue.GetMax()-p_fatigue) + "/" + theCharacterUIManager.fatigue.GetMax();
        apPrev.text = (theCharacterUIManager.ap.GetMax()-p_ap) + "/" + theCharacterUIManager.ap.GetMax();
    }
}
