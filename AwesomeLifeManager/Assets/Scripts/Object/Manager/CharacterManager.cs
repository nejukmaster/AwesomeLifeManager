using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    public AP ap;
    public Fatigue fatigue;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
}
