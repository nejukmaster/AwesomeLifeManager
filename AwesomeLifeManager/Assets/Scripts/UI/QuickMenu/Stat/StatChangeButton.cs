using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

public class StatChangeButton : MonoBehaviour
{
    public static StatChangeButton activatedButton;

    public float speed = 15f;
    public float downYPosRate = -50/1600;
    public bool setActivatedButtonThis = false;
    public ViewerContainer viewerContainer;
    public int viewerNum = 0;

    [SerializeField] Color deactiveColor;
    [SerializeField] Color activeColor;
    RectTransform UI_rect;
    float downYPos;
    Coroutine co;

    public void Start()
    {
        UI_rect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        downYPos = UI_rect.rect.height * downYPosRate;
        if(setActivatedButtonThis)
            activatedButton = this;
    }

    public void OnClick()
    {
        if(activatedButton != this)
        {
            activatedButton.Deactive();
            activatedButton = this;
            Active();
            UIManager.instance.externalListenerFired = true;
        }
    }

    public void Active()
    {
        if (co != null)
            StopCoroutine(co);
        co = StartCoroutine(ActiveCo());
    }

    public void Deactive()
    {
        if (co != null)
            StopCoroutine(co);
        co = StartCoroutine(DeactiveCo());
    }

    IEnumerator DeactiveCo()
    {
        viewerContainer.viewer[viewerNum].GetObj().gameObject.SetActive(false);
        this.GetComponent<Image>().color = deactiveColor;
        RectTransform t_rect = this.GetComponent<RectTransform>();
        while(t_rect.anchoredPosition.y > downYPos)
        {
            t_rect.anchoredPosition = Vector2.Lerp(t_rect.anchoredPosition, new Vector2(t_rect.anchoredPosition.x, downYPos),speed * Time.deltaTime);
            yield return null;
        }
        t_rect.anchoredPosition = new Vector2(t_rect.anchoredPosition.x, downYPos);
        viewerContainer.viewer[viewerNum].DeclareBox();
    }

    IEnumerator ActiveCo()
    {
        viewerContainer.viewer[viewerNum].GetObj().gameObject.SetActive(true);
        this.GetComponent<Image>().color = activeColor;
        RectTransform t_rect = this.GetComponent<RectTransform>();
        if(t_rect.anchoredPosition.y < 0)
        {
            t_rect.anchoredPosition = Vector2.Lerp(t_rect.anchoredPosition, new Vector2(t_rect.anchoredPosition.x, 0), speed * Time.deltaTime);
            yield return null;
        }
        t_rect.anchoredPosition = new Vector2(t_rect.anchoredPosition.x, 0);
        viewerContainer.viewer[viewerNum].GenBox();
    }
}
