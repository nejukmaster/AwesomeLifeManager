using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnProcessPopup : MonoBehaviour
{
    public TextMeshProUGUI planName;
    public Animator spriteAnimation;
    public LogScroll logScroll;

    ObjectPool theObjectPool;
    int pibot = 0;

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
        logScroll.updateObjs<TextMeshProUGUI>();
        pibot++;
    }
}
