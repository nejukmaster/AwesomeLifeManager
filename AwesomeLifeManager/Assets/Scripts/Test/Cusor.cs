using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cusor : MonoBehaviour
{
    Vector2 referenceSize = new Vector2(900,1600);
    // Update is called once per frame
    void Update()
    {
        float i_width = Input.mousePosition.x/Screen.width;
        float i_height = Input.mousePosition.y/Screen.height;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(referenceSize.x * i_width,
                                                                    referenceSize.y * i_height);
    }
}
