using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUIManager : MonoBehaviour
{
    public static CharacterUIManager instance;


    public void Start()
    {
        instance = this;
    }

    public void SetParam(){
        CharacterManager.instance.ap.SetParam();
        CharacterManager.instance.fatigue.SetParam();
    }
}
