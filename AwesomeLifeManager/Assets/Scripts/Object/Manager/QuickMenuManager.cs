using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickMenuManager : MonoBehaviour
{
    public static QuickMenuManager instance;

    public QuickMenuButton activatedButton;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
}
