using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;

public class TurnProcessPopup : MonoBehaviour
{
    public TextMeshProUGUI planName;
    public Animator spriteAnimation;
    public LogScroll logScroll;
    public GameObject exitButton;

    ObjectPool theObjectPool;
    int pibot = 0;
    Coroutine co;

    private void Start()
    {
        theObjectPool = ObjectPool.instance;
    }

    public void AddLog(string p_txt)
    {
        GameObject t_box = theObjectPool.logTextQueue.Dequeue();
        t_box.SetActive(true);
        t_box.GetComponent<TextMeshProUGUI>().text = p_txt;
        RectTransform t_rect = t_box.GetComponent<RectTransform>();
        t_rect.anchoredPosition = new Vector2(0, -1 * pibot * t_rect.rect.height);
        if(t_rect.anchoredPosition.y <= -1f * logScroll.GetComponent<RectTransform>().rect.height)
        {
            if (co != null)
                StopCoroutine(co);
            co = StartCoroutine(logScroll.ScrollCo(-1 * logScroll.GetEndPos().y));
        }
        logScroll.updateObjs<TextMeshProUGUI>();
        pibot++;
    }

    public void OnExitBtnClick()
    {
        Init();
        this.gameObject.SetActive(false);
    }

    public void Init()
    {
        logScroll.objGroup.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        for(int i = 0; i < logScroll.objs.Length; i++)
        {
            theObjectPool.logTextQueue.Enqueue(logScroll.objs[i].gameObject);
            logScroll.objs[i].gameObject.SetActive(false);
        }
        exitButton.SetActive(false);
        pibot = 0;
        co = null;
    }
}
