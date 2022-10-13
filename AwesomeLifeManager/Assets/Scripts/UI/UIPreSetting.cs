using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//비활성화 상태인 UI중에 게임시작과 동시에 재조정될 필요가 있는 UI를 재조정합니다.
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
