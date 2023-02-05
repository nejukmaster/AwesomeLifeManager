using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickMenuManager : Manager
{
    public static QuickMenuManager instance;

    public QuickMenuButton activatedButton;

    public override void Init()
    {
        activatedButton = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
}
