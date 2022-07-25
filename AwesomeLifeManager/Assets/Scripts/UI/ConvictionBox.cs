using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*  가치관을 표기하는 디스플레이 박스의 코드예요.   */
public class ConvictionBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;

    public static void Generate(string name){
        GameObject t_box = ObjectPool.instance.conviction_boxQueue.Dequeue();
        t_box.SetActive(true);
        t_box.GetComponent<ConvictionBox>().tmp.text = name;
        ConvictionBoxContainer.instance.AddBox(t_box.gameObject);
    }
}
