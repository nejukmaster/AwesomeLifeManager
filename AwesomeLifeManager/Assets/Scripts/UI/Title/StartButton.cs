using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class StartButton : MonoBehaviour
{
    [SerializeField] GameObject TitleUI;
    [SerializeField] GameObject MainUI;
    [SerializeField] SpriteAtlas Atlas;

    public bool activate = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Activate()
    {
        if (!activate)
        {
            Image t_img = GetComponent<Image>();
            t_img.sprite = Atlas.GetSprite("StartBtn-Activate");
            activate = true;
        }
    }

    void Deactivate()
    {
        if (activate)
        {
            Image t_img = GetComponent<Image>();
            t_img.sprite = Atlas.GetSprite("StartBtn-Deactivate");
            activate = false;
        }
    }

    public void OnClick()
    {
        TitleUI.SetActive(false);
        MainUI.SetActive(true);
        GameManager.instance.InitGame();
    }
}
