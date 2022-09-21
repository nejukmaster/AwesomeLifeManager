using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalenderCellWindow : MonoBehaviour
{
    CalenderCell currentCell;
    [SerializeField] TextMeshProUGUI date;
    [SerializeField] TextMeshProUGUI settedPlan;
    public void SetActive(bool p_bool, CalenderCell p_cell)
    {
        gameObject.SetActive(p_bool);
        if (p_bool)
        {
            currentCell = p_cell;
            date.text = currentCell.tmp.text;
            if (currentCell.insertedPlan != null)
            {
                settedPlan.text = currentCell.insertedPlan.name;
            }
            else
            {
                settedPlan.text = "Have not set Plan in this date yet...";
            }
        }
    }

    public void OnClick()
    {
        SetActive(false, null);
    }
}
