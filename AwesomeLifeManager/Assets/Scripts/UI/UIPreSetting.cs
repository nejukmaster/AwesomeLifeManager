using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPreSetting : MonoBehaviour
{
    [SerializeField] UIScaler[] uiScalers;
    [SerializeField] UIReplacer[] uiReplacers;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < uiScalers.Length; i++)
        {
            uiScalers[i].PreResizing();
        }   
        for(int i = 0; i < uiReplacers.Length; i++)
        {
            uiReplacers[i].PreReplacing();
        }
    }
}
