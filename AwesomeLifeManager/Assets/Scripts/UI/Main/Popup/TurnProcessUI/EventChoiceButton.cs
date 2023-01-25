using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EventChoiceButton : MonoBehaviour
{
    static Coroutine coroutine = null;

    Vector2 originSize;
    Vector2 originPos;

    [SerializeField] int choiceIndex;
    [SerializeField] EventPopup popup;
    [SerializeField] float yCoef;
    [SerializeField] float typingSpeed = 0.2f;

    private void Awake()
    {
        originSize = GetComponent<RectTransform>().sizeDelta;
        originPos = GetComponent<RectTransform>().anchoredPosition;
    }
    public void OnClick()
    {
        if(coroutine == null)
        {
            coroutine = StartCoroutine(ChoiceCo());
        }
    }

    IEnumerator ChoiceCo()
    {
        Choice t_choice = popup.currentEvent.choices[choiceIndex];
        GameObject t_gameObj = popup.choices[choiceIndex];
        TextMeshProUGUI t_tmp = t_gameObj.GetComponentInChildren<TextMeshProUGUI>();
        Vector2 bottom = t_gameObj.GetComponent<RectTransform>().anchoredPosition;
        for(int i = 0; i < popup.choices.Length; i++)
        {
            if (popup.choices[i].GetComponent<RectTransform>().anchoredPosition.y < bottom.y) bottom = popup.choices[i].GetComponent<RectTransform>().anchoredPosition;
            if (popup.choices[i].GetComponentInChildren<TextMeshProUGUI>() != t_tmp) popup.choices[i].SetActive(false);
        }
        Image t_cont = t_gameObj.GetComponent<Image>();
        RectTransform t_rect = t_cont.GetComponent<RectTransform>();
        float destY = t_cont.GetComponent<RectTransform>().rect.height * yCoef;
        while(Mathf.Abs(t_rect.rect.height - destY) >= 0.2f)
        {
            t_rect.anchoredPosition = Vector2.Lerp(t_rect.anchoredPosition,
                                                    bottom,
                                                    10f * Time.deltaTime);
            t_rect.sizeDelta = Vector2.Lerp(t_rect.sizeDelta,
                                            new Vector2(t_rect.rect.width,destY),
                                            10f * Time.deltaTime);
            yield return null;
        }
        t_rect.sizeDelta = new Vector2(t_rect.rect.width, destY);
        t_tmp.text = "";
        if(t_choice.texture != null)
            popup.eventIllustration.sprite = t_choice.texture;
        for(int i = 0; i < t_choice.text.Length; i++)
        {
            t_tmp.text = t_choice.text.Substring(0, i + 1);
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(2.0f);
        popup.Container.SetActive(false);
        popup.SetActive(false, null);
        coroutine = null;
    }

    public void Initialize()
    {
        this.GetComponent<RectTransform>().sizeDelta = originSize;
        this.GetComponent<RectTransform>().anchoredPosition = originPos;
    }
}
