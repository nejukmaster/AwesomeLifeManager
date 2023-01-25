using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarningPopup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;
    TurnManager theTurnManager;

    void Awake(){
        theTurnManager = TurnManager.instance;
    }

    public void SetActive(bool p_bool, string p_text){
        this.gameObject.SetActive(p_bool);
        if(p_text != null){
            tmp.text = p_text;
        }
        if(p_bool)
            UI.ToggleSubUI(theTurnManager.MainUI, false);
        else
            UI.ToggleSubUI(theTurnManager.MainUI, true);
    }

    public void  OnClick(){
        SetActive(false, null);
    }
}
