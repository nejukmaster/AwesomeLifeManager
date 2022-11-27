using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryChangeButton : MonoBehaviour
{
    public static CategoryChangeButton activatedButton;

    public bool isActivated = false;

    [SerializeField] CardViewer cardViewer;
    [SerializeField] Color activeColor;
    [SerializeField] Color deactiveColor;

    public void OnClick()
    {
        if (activatedButton != this)
        {
            if(activatedButton != null)
                activatedButton.Deactive();
            Active();
            activatedButton = this;
        }
    }

    public void Active()
    {
        cardViewer.gameObject.SetActive(true);
        cardViewer.GenIcons();
        this.GetComponent<Image>().color = activeColor;
    }

    public void Deactive()
    {
        cardViewer.DeleteIcons();
        cardViewer.gameObject.SetActive(false);
        this.GetComponent<Image>().color = deactiveColor;
    }
}
