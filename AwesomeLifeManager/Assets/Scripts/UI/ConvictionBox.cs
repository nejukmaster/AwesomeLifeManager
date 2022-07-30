using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*  가치관을 표기하는 디스플레이 박스의 코드예요.   */
public class ConvictionBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;

    [SerializeField] Material m_mat;

    [SerializeField] Image img;

    public static void Generate(string name){
        GameObject t_box = ObjectPool.instance.conviction_boxQueue.Dequeue();
        t_box.SetActive(true);
        t_box.GetComponent<ConvictionBox>().tmp.text = name;
        ConvictionBoxContainer.instance.AddBox(t_box.gameObject);
        t_box.GetComponent<ConvictionBox>().StartCoroutine(t_box.GetComponent<ConvictionBox>().Gen());
    }

    public IEnumerator Gen(){
        for(int i = 0; i < 10; i ++){
            m_mat.SetFloat("_snap", 0-i);
            yield return new WaitForSeconds(0.2f);
        }
        tmp.gameObject.SetActive(true);
        img.material = null;
    }
}
