using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planed : MonoBehaviour
{
    public static Planed instance;
    
    void Awake(){
        instance = this;
    }
}
