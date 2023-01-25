using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Manager[] managers;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitGame()
    {
        try
        {
            foreach (Manager m in managers)
            {
                m.Init();
            }
        }
        catch (Exception err)
        {

        }
    }
}
