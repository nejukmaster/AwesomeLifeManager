using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PersonalityBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;

    [SerializeField] Material m_mat;

    [SerializeField] Image img;

    public static void Generate(string name){
        GameObject t_box = ObjectPool.instance.personality_boxQueue.Dequeue();
        t_box.SetActive(true);
        t_box.GetComponent<PersonalityBox>().tmp.text = name;
        PersonalityBoxContainer.instance.AddBox(t_box.gameObject);
         t_box.GetComponent<PersonalityBox>().StartCoroutine(t_box.GetComponent<PersonalityBox>().Gen());
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
