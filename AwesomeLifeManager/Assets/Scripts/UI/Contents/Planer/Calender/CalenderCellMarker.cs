using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalenderCellMarker : MonoBehaviour
{
    [SerializeField] RectTransform cell;
    [SerializeField] Vector2 scalihngRate;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(cell.rect.width * scalihngRate.x, cell.rect.height * scalihngRate.y);   
    }
}
