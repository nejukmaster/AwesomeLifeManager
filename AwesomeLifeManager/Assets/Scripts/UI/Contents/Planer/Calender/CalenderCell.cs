using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalenderCell : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI tmp;
    Calender calender;
    // Start is called before the first frame update

    public void Awake()
    {
        calender = GetComponentInParent<Calender>();
    }

    public void Gen(int p_num, Vector2 p_pos)
    {
        this.gameObject.SetActive(true);
        this.GetComponent<RectTransform>().anchoredPosition = p_pos;
        tmp.text = p_num.ToString();
    }
}
