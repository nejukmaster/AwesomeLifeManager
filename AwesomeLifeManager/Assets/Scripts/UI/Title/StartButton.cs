using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] GameObject TitleUI;
    [SerializeField] GameObject MainUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        TitleUI.SetActive(false);
        MainUI.SetActive(true);
        GameManager.instance.InitGame();
    }
}
