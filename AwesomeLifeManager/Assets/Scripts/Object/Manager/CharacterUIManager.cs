using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUIManager : MonoBehaviour
{
    public static CharacterUIManager instance;

    [SerializeField] public AP ap;
    [SerializeField] public Fatigue fatigue;

    public void Start()
    {
        instance = this;
    }

    public void SetParam(){
        ap.SetParam();
        fatigue.SetParam();
    }
}
