using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public string name;
    public bool enabled;

    public Buff(string name)
    {
        this.name = name;
    }
}
public class BuffManager : Manager
{
    public Dictionary<string, Buff> buffs = new Dictionary<string, Buff>();

    public override void Init()
    {

    }

    private void mapping()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
