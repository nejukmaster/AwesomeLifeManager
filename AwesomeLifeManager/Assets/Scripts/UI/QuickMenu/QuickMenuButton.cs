using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickMenuButton : MonoBehaviour
{
    public GameObject activationObj;

    UIManager theUIManager;
    QuickMenuManager theQuickMenuManager;
    [SerializeField] GameObject deactiveUI;
    [SerializeField] RectTransform container;
    [SerializeField] ContentsBoxPopup contentsBoxPopup;
    [SerializeField] Container contentsContainer;
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
            OnClickQuickmenu();
            trig = !trig;
            UI.ToggleSubUI(deactiveUI, false);
        }
        else
        {
            if (theQuickMenuManager.activatedButton == this)
            {
                UI.ToggleSubUI(deactiveUI, true);
                theQuickMenuManager.activatedButton = null;
            }
            container.sizeDelta = new Vector2(container.rect.width, container.rect.height / 3);
            contentsBoxPopup.SetActive(false,activationObj);
            contentsContainer.ZoomOut(false);
            OnExitQuickmenu();
            trig = !trig;
        }
        theUIManager.externalListenerFired = true;
    }

    public virtual void OnExitQuickmenu()
    {
        return;
    }

    public virtual void OnClickQuickmenu()
    {
        return;
    }
}
