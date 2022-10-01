using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickMenuButton : MonoBehaviour
{
    UIManager theUIManager;
    QuickMenuManager theQuickMenuManager;
    [SerializeField] RectTransform container;
    [SerializeField] ContentsBoxPopup contentsBoxPopup;
    [SerializeField] Container contentsContainer;
    [SerializeField] GameObject activationObj;
    bool trig = true;

    private void Start()
    {
        theUIManager = UIManager.instance;
        theQuickMenuManager = QuickMenuManager.instance;
    }

    public void OnClick()
    {
        if (trig)
        {
            if (theQuickMenuManager.activatedButton != null)
                theQuickMenuManager.activatedButton.OnClick();
            container.sizeDelta = new Vector2(container.rect.width, container.rect.height * 3);
            contentsBoxPopup.SetActive(true,activationObj);
            contentsContainer.ZoomIn(false);
            theQuickMenuManager.activatedButton = this;
            trig = !trig;
        }
        else
        {
            if (theQuickMenuManager.activatedButton == this)
                theQuickMenuManager.activatedButton = null;
            container.sizeDelta = new Vector2(container.rect.width, container.rect.height / 3);
            contentsBoxPopup.SetActive(false,activationObj);
            contentsContainer.ZoomOut(false);
            trig = !trig;
        }
        theUIManager.externalListenerFired = true;
    }
}
