using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCard : MonoBehaviour
{
    private string cardImagePath = "Images/Action/Illustration";
    Hand hand;
    RectTransform uiCanvas;

    public bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponentInParent<Hand>();  
        uiCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2(Utility.Mapping(Input.mousePosition.x,new Vector2(0,Screen.width),new Vector2(0,uiCanvas.rect.width)), 
                                                                                Utility.Mapping(Input.mousePosition.y,new Vector2(0,Screen.height),new Vector2(0,uiCanvas.rect.height)))
                                                                  - hand.anchoredPos;
        }
    }
}
