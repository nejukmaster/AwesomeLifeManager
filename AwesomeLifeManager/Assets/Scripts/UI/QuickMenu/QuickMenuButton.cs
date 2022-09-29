using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickMenuButton : MonoBehaviour
{
    UIManager theUIManager;
    [SerializeField] RectTransform container;
    [SerializeField] GameObject contentsBoxPopup;
    [SerializeField] Container contentsContainer;
    bool trig = true;

    private void Start()
    {
        theUIManager = UIManager.instance;
    }

    public void OnClick()
    {
        if (trig)
        {
            container.sizeDelta = new Vector2(container.rect.width, container.rect.height * 3);
            contentsBoxPopup.SetActive(true);
            contentsContainer.ZoomIn(false);
            trig = !trig;
        }
        else
        {
            container.sizeDelta = new Vector2(container.rect.width, container.rect.height / 3);
            contentsBoxPopup.SetActive(false);
            contentsContainer.ZoomOut(false);
            trig = !trig;
        }
        theUIManager.externalListenerFired = true;
    }
}
