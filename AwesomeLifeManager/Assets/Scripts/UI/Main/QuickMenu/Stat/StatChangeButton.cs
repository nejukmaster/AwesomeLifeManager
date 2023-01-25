using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatChangeButton : MonoBehaviour
{
    public static StatChangeButton activatedButton;

    public float speed = 15f;
    public float downYPosRate = -50/1600;
    public bool setActivatedButtonThis = false;
    public int viewerNum = 0;

    [SerializeField] Color deactiveColor;
    [SerializeField] Color activeColor;
    [SerializeField] GameObject viewer;
    RectTransform UI_rect;
    float downYPos;
    Coroutine co;

    public void Awake()
    {
        UI_rect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        downYPos = UI_rect.rect.height * downYPosRate;
    }

    public void OnClick()
    {
        if(activatedButton != this)
        {
            if (activatedButton != null)
            {
                activatedButton.Deactive(true);

            }
            activatedButton = this;
            Active(true);
            UIManager.instance.externalListenerFired = true;
        }
    }

    public void Active(bool p_bool)
    {
        if (co != null)
            StopCoroutine(co);
        co = StartCoroutine(ActiveCo(p_bool));
    }

    public void Deactive(bool p_bool)
    {
        if (p_bool)
        {
            if (co != null)
                StopCoroutine(co);
            co = StartCoroutine(DeactiveCo(p_bool));
        }
        else
        {
            viewer.GetComponent<Viewer>().DeclareBox();
            viewer.SetActive(false);
        }
    }

    IEnumerator DeactiveCo(bool p_bool)
    {
        viewer.GetComponent<Viewer>().DeclareBox();
        viewer.SetActive(false);
        this.GetComponent<Image>().color = deactiveColor;
        RectTransform t_rect = this.GetComponent<RectTransform>();
        while(t_rect.anchoredPosition.y > downYPos && p_bool)
        {
            t_rect.anchoredPosition = Vector2.Lerp(t_rect.anchoredPosition, new Vector2(t_rect.anchoredPosition.x, downYPos),speed * Time.deltaTime);
            yield return null;
        }
        t_rect.anchoredPosition = new Vector2(t_rect.anchoredPosition.x, downYPos);
    }

    IEnumerator ActiveCo(bool p_bool)
    {
        this.GetComponent<Image>().color = activeColor;
        RectTransform t_rect = this.GetComponent<RectTransform>();
        if(t_rect.anchoredPosition.y < 0 && p_bool)
        {
            t_rect.anchoredPosition = Vector2.Lerp(t_rect.anchoredPosition, new Vector2(t_rect.anchoredPosition.x, 0), speed * Time.deltaTime);
            yield return null;
        }
        t_rect.anchoredPosition = new Vector2(t_rect.anchoredPosition.x, 0);
        viewer.SetActive(true);
        viewer.GetComponent<Viewer>().GenBox();
    }
}
