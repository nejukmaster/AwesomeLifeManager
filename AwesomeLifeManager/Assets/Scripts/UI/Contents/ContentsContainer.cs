using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsContainer : MonoBehaviour
{
    [SerializeField] ContentsBox[] boxes;

    public void Fade(){
        StartCoroutine(FadeCo());
    }

    public void Reveal(){
        StartCoroutine(RevealCo());
    }

    IEnumerator FadeCo(){
        int i = 0;
        while(i < boxes.Length){
            boxes[i].Fade();
            i++;
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator RevealCo(){
        int i = 0;
        while(i < boxes.Length){
            boxes[i].Reveal();
            i++;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
