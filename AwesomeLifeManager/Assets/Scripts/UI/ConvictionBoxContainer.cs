using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  가치관의 디스플레이박스를 실제로 배치하는 클래스예요 */
public class ConvictionBoxContainer : MonoBehaviour
{
    public static ConvictionBoxContainer instance;

    [SerializeField] GameObject box;

    int pibot = 0;

    void Start(){
        instance = this;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.A)){
            RectTransform t_rect = (RectTransform)box.transform;
            Debug.Log(t_rect.localPosition);
        }
    }

    public void AddBox(GameObject p_box){
        RectTransform t_rect = (RectTransform)p_box.transform;
        p_box.transform.localPosition = p_box.transform.localPosition + new Vector3(0,t_rect.rect.height * pibot,0);
        pibot ++;
    }
}
