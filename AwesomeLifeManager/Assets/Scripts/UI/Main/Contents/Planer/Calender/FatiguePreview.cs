using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FatiguePreview : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] Calender calender;

    FatigueManager theFatigueManager;

    private void Start()
    {
        theFatigueManager = FatigueManager.instance;
        Setting();
    }

    public void Setting()
    {
        tmp.text = theFatigueManager.Fatigue + "/" + theFatigueManager.MaxFatigue;
    }
}
