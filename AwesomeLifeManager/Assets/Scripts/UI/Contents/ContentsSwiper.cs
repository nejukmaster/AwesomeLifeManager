using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsSwiper : UI
{
    [SerializeField] ContentsContainer[] Containers;
    public override bool onClickDown(Vector2 pos){
        return false;
    }

    public override bool onClickUp(float f, Vector2 pos){
        if(f>400){
            StartCoroutine(ChangeContainerCo());
            return true;
        }
        return false;
    }

    public override bool onSwipe(Vector2 sp, Vector2 ep){
        return false;
    }

    IEnumerator ChangeContainerCo(){
        Containers[0].Fade();
        yield return new WaitForSeconds(0.3f);
        Containers[1].Reveal();
    }
}
