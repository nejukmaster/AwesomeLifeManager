using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PersonalityBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;

    public static void Generate(string name){
        GameObject t_box = ObjectPool.instance.boxQueue.Dequeue();
        t_box.SetActive(true);
        t_box.GetComponent<PersonalityBox>().tmp.text = name;
        PersonalityBoxContainer.instance.AddBox(t_box.gameObject);
    }
}
