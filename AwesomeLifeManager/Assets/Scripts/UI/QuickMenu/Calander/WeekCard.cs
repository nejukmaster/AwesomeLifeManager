using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeekCard : Scroll
{
    public int weekNum = 0;
    public float currentX;

    [SerializeField] Button flipButton;
    TurnManager theTurnManager;
    ObjectPool theObjectPool;
    PlanContainer thePlanContainer;

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
                    t_box.transform.SetParent(this.objGroup);
                    t_box.GetComponentInChildren<TextMeshProUGUI>().text = (i+1) + "일차: "+ theTurnManager.currentTurn.settedPlan[i].name;
                    RectTransform t_rect = t_box.GetComponent<RectTransform>();
                    t_rect.anchoredPosition = new Vector2(0, -1 * t_rect.rect.height * pibot);
                    pibot++;
                }
            }
        }
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
        StartCoroutine(FlipCo());
    }

    public void OnFlipButtonClick()
    {
        StartCoroutine(UnflipCo());
    }

    IEnumerator FlipCo()
    {
        thePlanContainer.dropShadow.SetActive(false);
        thePlanContainer.active = false;
        while (this.GetComponent<RectTransform>().localScale.x > 0.1)
        {
            this.GetComponent<RectTransform>().localScale = Vector2.Lerp(this.GetComponent<RectTransform>().localScale,
                                                                         new Vector2(0, this.GetComponent<RectTransform>().localScale.y),
                                                                         3f * Time.deltaTime);
            yield return null;
        }
        this.GetComponent<Button>().enabled = false;
        while (this.GetComponent<RectTransform>().localScale.x < 0.9)
        {
            this.GetComponent<RectTransform>().localScale = Vector2.Lerp(this.GetComponent<RectTransform>().localScale,
                                                                         new Vector2(1, this.GetComponent<RectTransform>().localScale.y),
                                                                         3f * Time.deltaTime);
            yield return null;
        }
        flipButton.gameObject.SetActive(true);
        this.active = true;
        genPlanBox();
    }

    IEnumerator UnflipCo()
    {
        thePlanContainer.dropShadow.SetActive(false);
        while (this.GetComponent<RectTransform>().localScale.x > 0.1)
        {
            this.GetComponent<RectTransform>().localScale = Vector2.Lerp(this.GetComponent<RectTransform>().localScale,
                                                                         new Vector2(0, this.GetComponent<RectTransform>().localScale.y),
                                                                         3f * Time.deltaTime);
            yield return null;
        }
        flipButton.gameObject.SetActive(false);
        this.active = false;
        declarePlanBox();
        while (this.GetComponent<RectTransform>().localScale.x < 0.9)
        {
            this.GetComponent<RectTransform>().localScale = Vector2.Lerp(this.GetComponent<RectTransform>().localScale,
                                                                         new Vector2(1, this.GetComponent<RectTransform>().localScale.y),
                                                                         3f * Time.deltaTime);
            yield return null;
        }
        this.GetComponent<Button>().enabled = true;
        thePlanContainer.active = true;
    }
}
