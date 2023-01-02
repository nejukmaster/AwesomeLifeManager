using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalenderCell : MonoBehaviour
{
    public List<Plan> insertedPlan = new List<Plan>();
    public Calender calender;
    public TextMeshProUGUI tmp;
    [SerializeField] GameObject holdMarker;
    [SerializeField] Image planMarker;

    private void Update()
    {
        
    }

    public void HoldeOn()
    {
        holdMarker.SetActive(true);
    }

    public void HoldOut()
    {
        holdMarker.SetActive(false);
    }

    public bool InsertPlan(Plan p_plan)
    {
        if (insertedPlan.Count < 7)
        {
            insertedPlan.Add(p_plan);
            planMarker.GetComponent<RectTransform>().localScale = new Vector3(insertedPlan.Count / 7f, 1f, 0f);
            return true;
        }
        else
            return false;
    }

    public void DeletePlan(Plan p_plan)
    {
        insertedPlan.Remove(p_plan);
        planMarker.GetComponent<RectTransform>().localScale = new Vector3(insertedPlan.Count / 7f, 1f, 0f);
    }

    public void Init()
    {
        insertedPlan = new List<Plan>();
        planMarker.GetComponent<RectTransform>().localScale = new Vector3(insertedPlan.Count / 7f, 1f, 0f);
        HoldOut();
    }
}
