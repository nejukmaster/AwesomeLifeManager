using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//각 주의 일정을 확인할 수 있는 UI 입니다.
public class WeekCard : Scroll
{
    public int weekNum = 0;
    public float currentX;
    public bool fliped = false;

    [SerializeField] Button flipButton;
    TurnManager theTurnManager;
    ObjectPool theObjectPool;
    PlanContainer thePlanContainer;
    Coroutine co;

    private void Awake()
    {
        theTurnManager = TurnManager.instance;
        theObjectPool = ObjectPool.instance;
        thePlanContainer = this.GetComponentInParent<PlanContainer>();
        this.active = false;
    }

    public void genPlanBox()
    {
        if (theTurnManager.currentTurn.settedPlan.Count > 0)
        {
            int pibot = 0;
            for (int i = 0; i < 7; i++)
            {
                int _i = weekNum * 7 + i;
                if (theTurnManager.currentTurn.settedPlan[_i] != null)
                {
                    GameObject t_box = theObjectPool.weekPlanQueue.Dequeue();
                    t_box.SetActive(true);
                    t_box.transform.SetParent(this.objGroup, false);
                    t_box.GetComponentInChildren<TextMeshProUGUI>().text = (i+1) + "일차: "+ theTurnManager.currentTurn.settedPlan[_i].name;
                    RectTransform t_rect = t_box.GetComponent<RectTransform>();
                    t_rect.localScale = Vector2.one;
                    t_rect.anchoredPosition = new Vector2(0, -1 * t_rect.rect.height * pibot);
                    if(t_box.GetComponentInChildren<Toggle>() != null)
                        t_box.GetComponentInChildren<Toggle>().gameObject.SetActive(false);
                    pibot++;
                }
            }
        }
        updateObjs<PlanBox>();
    }

    public void declarePlanBox()
    {
        if(objGroup.transform.childCount > 0)
        {
            for(int i = 0; i < objGroup.transform.childCount; i++)
            {
                GameObject t_box = objGroup.transform.GetChild(i).gameObject;
                t_box.SetActive(false);
                theObjectPool.weekPlanQueue.Enqueue(t_box);
            }
        }
        updateObjs<PlanBox>();
    }

    public override void onEndSwipe()
    {
        return;
    }

    public override void onStartSwipe()
    {
        return;
    }

    public void OnClick()
    {
        Flip(true);
    }

    public void OnFlipButtonClick()
    {
        Unflip(true);
    }

    public void Unflip(bool isActivatedAnime)
    {
        if (co != null)
            StopCoroutine(co);
        co = StartCoroutine(UnflipCo(isActivatedAnime));
    }

    public void Flip(bool isActivatedAnime)
    {
        if (co != null)
            StopCoroutine(co);
        co = StartCoroutine(FlipCo(isActivatedAnime));
    }

    IEnumerator FlipCo(bool isActivatedAnime)
    {
        thePlanContainer.dropShadow.SetActive(false);
        thePlanContainer.active = false;
        while (this.GetComponent<RectTransform>().localScale.x > 0.1 && isActivatedAnime)
        {
            this.GetComponent<RectTransform>().localScale = Vector2.Lerp(this.GetComponent<RectTransform>().localScale,
                                                                         new Vector2(0, this.GetComponent<RectTransform>().localScale.y),
                                                                         3f * Time.deltaTime);
            yield return null;
        }
        while (this.GetComponent<RectTransform>().localScale.x < 0.9 && isActivatedAnime)
        {
            this.GetComponent<RectTransform>().localScale = Vector2.Lerp(this.GetComponent<RectTransform>().localScale,
                                                                         new Vector2(1, this.GetComponent<RectTransform>().localScale.y),
                                                                         3f * Time.deltaTime);
            yield return null;
        }
        flipButton.gameObject.SetActive(true);
        thePlanContainer.dropShadow.SetActive(true);
        this.active = true;
        this.GetComponent<RectTransform>().localScale = Vector2.one;
        genPlanBox();
        fliped = true;
        yield return null;
    }

    IEnumerator UnflipCo(bool isActivatedAnime)
    {
        thePlanContainer.dropShadow.SetActive(false);
        while (this.GetComponent<RectTransform>().localScale.x > 0.1 && isActivatedAnime)
        {
            this.GetComponent<RectTransform>().localScale = Vector2.Lerp(this.GetComponent<RectTransform>().localScale,
                                                                         new Vector2(0, this.GetComponent<RectTransform>().localScale.y),
                                                                         3f * Time.deltaTime);
            yield return null;
        }
        flipButton.gameObject.SetActive(false);
        declarePlanBox();
        objGroup.anchoredPosition = new Vector2(objGroup.anchoredPosition.x, 0);
        this.active = false;
        while (this.GetComponent<RectTransform>().localScale.x < 0.9 && isActivatedAnime)
        {
            this.GetComponent<RectTransform>().localScale = Vector2.Lerp(this.GetComponent<RectTransform>().localScale,
                                                                         new Vector2(1, this.GetComponent<RectTransform>().localScale.y),
                                                                         3f * Time.deltaTime);
            yield return null;
        }
        thePlanContainer.active = true;
        thePlanContainer.dropShadow.SetActive(true);
        this.GetComponent<RectTransform>().localScale = Vector2.one;
        fliped = false;
        yield return null;
    }
}
