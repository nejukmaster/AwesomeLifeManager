using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : Manager
{
    public static ChoiceManager instance;

    public Dictionary<string, Choice> choices = new Dictionary<string, Choice>();
    public override void Init()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
