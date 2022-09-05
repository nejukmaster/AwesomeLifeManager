using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsBox : MonoBehaviour
{
    [SerializeField] float firstXPos;
    [SerializeField] float fadedXPos;
    bool pass_collider = false;
    float speed = 8;
    float dir = -1f;

    public void SetDir(int dir){
        this.dir = (float)dir;
    }

    public void Fade(){
        StartCoroutine(FadeCo());
    }

    public void Reveal(){
        StartCoroutine(RevealCo());
    }

    IEnumerator FadeCo(){
        Vector3 t_pos = new Vector3(dir*fadedXPos,this.transform.localPosition.y, this.transform.localPosition.z);
        while(Vector3.Distance(this.transform.localPosition, t_pos) >= 0.1f){
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition,
                                                        t_pos,
                                                        speed * Time.deltaTime);
            yield return null;
        }
        this.transform.localPosition = t_pos;
    }

    IEnumerator RevealCo(){
        Vector3 t_pos = new Vector3(firstXPos,this.transform.localPosition.y, this.transform.localPosition.z);
        while(Vector3.Distance(this.transform.localPosition, t_pos) >= 0.1f){
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition,
                                                        t_pos,
                                                        speed * Time.deltaTime);
            yield return null;
        }
        this.transform.localPosition = t_pos;
    }
}
