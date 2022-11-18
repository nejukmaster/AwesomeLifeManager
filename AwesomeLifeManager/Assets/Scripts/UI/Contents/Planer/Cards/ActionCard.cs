using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ActionCard : MonoBehaviour
{
    private bool canClick = true;
    private float slideSpeed = 9.5f;
    private Vector2 anchoredPos;
    private Coroutine currentCoroutine;
    RectTransform uiCanvas;
    Hand hand;

    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI nameBlank;
    [SerializeField] Image illustration;
    [SerializeField] Image categoryImg;
    [SerializeField] TextMeshProUGUI cost;
    
    public Calender calender;
    public bool activated = false;
    public CalenderCell currentCell;
    public CardInform inform;

    private void Awake()
    {
        Material MaterialInstance = Instantiate(this.GetComponentInChildren<Image>().materialForRendering);
        foreach (Image i in GetComponentsInChildren<Image>())
        {
            i.material = MaterialInstance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponentInParent<Hand>();  
        uiCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    public CalenderCell CheckHolding()
    {
        try
        {
            Vector2 t_pos = this.GetComponent<RectTransform>().anchoredPosition + hand.anchoredPos;
            Vector2 s_pos = new Vector2(calender.anchoredPos.x - calender.GetComponent<RectTransform>().rect.width / 2, calender.anchoredPos.y - calender.GetComponent<RectTransform>().rect.height / 2);
            Vector2 e_pos = new Vector2(calender.anchoredPos.x + calender.GetComponent<RectTransform>().rect.width / 2, calender.anchoredPos.y + calender.GetComponent<RectTransform>().rect.height / 2);
            if (t_pos.x >= s_pos.x && t_pos.y >= s_pos.y && t_pos.x <= e_pos.x && t_pos.y <= e_pos.y)
            {
                int x_gride = (int)((t_pos.x - s_pos.x) / calender.cells[0].GetComponent<RectTransform>().rect.width);
                int y_gride = (int)((e_pos.y - t_pos.y) / calender.cells[0].GetComponent<RectTransform>().rect.height);
                return calender.cells[y_gride * 7 + x_gride];
            }
            return null;
        }
        catch(IndexOutOfRangeException)
        {
            return null;
        }
    }

    public void Slide(Vector2 p_dest)
    {
        if(currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(SlideCo(p_dest));
    }

    public void SettingCard()
    {
        nameBlank.text = inform.name;
        description.text = inform.description;
        cost.text = inform.cost.ToString();
        Sprite t_sprite = CardManager.instance.illustrationAtlas.GetSprite(inform.illusteName);
        if (t_sprite != null)
        {
            illustration.sprite = t_sprite;
        }
        SpriteAtlas t_atlas = GetComponentInParent<UIManager>().IconAtlas;
        switch (inform.type)
        {
            case CardType.Action:
                categoryImg.sprite = t_atlas.GetSprite("Action");
                break;
            case CardType.Project:
                categoryImg.sprite = t_atlas.GetSprite("Project");
                break;
            case CardType.Event:
                categoryImg.sprite = t_atlas.GetSprite("Event");
                break;
            case CardType.Angel:
                categoryImg.sprite = t_atlas.GetSprite("Angel");
                break;
        }
    }

    public IEnumerator SlideCo(Vector2 p_dest)
    {
        while(Vector2.Distance(GetComponent<RectTransform>().anchoredPosition,p_dest) >= 0.1)
        {
            canClick = false;
            RectTransform t_rect = GetComponent<RectTransform>();
            t_rect.anchoredPosition = Vector2.Lerp(t_rect.anchoredPosition,
                                                    p_dest,
                                                    slideSpeed * Time.deltaTime);
            yield return null;
        }
        GetComponent<RectTransform>().anchoredPosition = p_dest;
        canClick = true;
    }

    public void Burn()
    {
        StartCoroutine(BurningCo());
    }

    public IEnumerator BurningCo()
    {
        float a = 1;
        Material t_mat = this.GetComponentInChildren<Image>().materialForRendering;
        while(a - 0.1f > t_mat.GetFloat("_AlphaCut"))
        {
            t_mat.SetFloat("_AlphaCut", Vector2.Lerp(new Vector2(0, t_mat.GetFloat("_AlphaCut")),
                                        new Vector2(0, a + 0.1f),
                                        1.2f * Time.deltaTime).y);
            yield return null;
        }
        t_mat.SetFloat("_AlphaCut", 1);
        this.gameObject.SetActive(false);
    }
}
