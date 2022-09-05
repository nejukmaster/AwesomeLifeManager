using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField] ActionMenu menu;
    [SerializeField] ActionReveal revealButton;
    [SerializeField] Image sprite;
    public Vector2 revPos;
    public Vector2 disPos;
    public Vector2 hidePos;
    public Vector2 sickPos;
    public int currentSlot = -1;
    private float move_speed = 18;

    public void Reveal(){
        StartCoroutine(ReavalCo());
    }

    public void Dissap(){
        StartCoroutine(DissapCo());
    }

    public void Scaling(float p_rate){
        this.transform.localScale = new Vector3(p_rate, p_rate, 0);
    }

    IEnumerator ReavalCo(){
        while(Vector2.Distance(revPos,this.transform.localPosition) >= 0.001f){
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition,
                                                        revPos,
                                                        move_speed * Time.deltaTime);
            yield return null;
        }
        this.transform.localPosition = revPos;
        if(menu != null){
            menu.SettingButton();
        }
        if(revealButton != null){
            revealButton.canPressButton = true;
        }
    }

    IEnumerator DissapCo(){
            while(Vector2.Distance(disPos,this.transform.localPosition) >= 0.001f){
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition,
                                                        disPos,
                                                        move_speed * Time.deltaTime);
                yield return null;
            }
            this.transform.localPosition = disPos;
            if(menu != null){
                menu.gameObject.SetActive(false);
            }
        }
}
