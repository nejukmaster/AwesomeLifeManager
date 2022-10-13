using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    //뒷배경의 크기를 해상도에 맞게 늘여줍니다.
    [SerializeField] RectTransform UICanvas;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(UICanvas.rect.width, UICanvas.rect.height);
    }
}
