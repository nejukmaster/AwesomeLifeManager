using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionReveal : UI
{
    [SerializeField] ActionMenu actionMenu;
    [SerializeField] GameObject menuContainer;
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] Transform risePoint;
    bool UporDown = true;
    float speed = 4;
    public bool canPressButton = true;

    public bool check_button_press = false;

    public override bool onClickDown(Vector2 pos){
        return false;
    }

    public void onClick(){
        UIManager.instance.externalListenerFired = true;
        if(UporDown){
            StartCoroutine(RiseCo());
        }
        else{
            StartCoroutine(DownCo());
            actionMenu.SetActive(false);
            actionMenu.keyDown = false;
        }
    }

    public override bool onClickUp(float f, Vector2 pos){
        return false;
    }

    public override bool onSwipe(Vector2 sp, Vector2 ep){
        return false;
    }

    IEnumerator RiseCo(){
            while(Vector2.Distance(this.transform.localPosition, risePoint.localPosition) >= 0.1f){
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition,
                                                            risePoint.localPosition,
                                                            speed * Time.deltaTime);
                canPressButton =false;
                yield return null;
            }
            this.transform.localPosition = risePoint.localPosition;
            menuContainer.transform.position = this.transform.position;
            actionMenu.SetActive(true);
            tmp.text = "X";
            UporDown = !UporDown;
    }

    IEnumerator DownCo(){
        if(canPressButton){
            while(Vector2.Distance(this.transform.localPosition, Vector2.zero) >= 0.1f){
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition,
                                                            Vector2.zero,
                                                            speed * Time.deltaTime);
                canPressButton = false;
                                                        
                menuContainer.transform.position = this.transform.position;
                yield return null;
            }
            this.transform.localPosition = Vector2.zero;
            tmp.text = "행동";
            UporDown = !UporDown;
            canPressButton = true;
        }
    }
}
