using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUIManager : MonoBehaviour
{
    [SerializeField] Hungry hungry;
    [SerializeField] Fatigue fatigue;

    public void SetParam(){
        hungry.SetParam();
        fatigue.SetParam();
    }
}
