using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventWindow : MonoBehaviour
{
    Vector3 upPosition;
    Vector3 downPosition;

    TextMeshProUGUI tmp = null;

    public int speed = 16;
    bool canClick = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        upPosition = Vector3.zero;
        downPosition = new Vector3(0,-1000,0);
        StartCoroutine(Up());
        Timer.instance.increase_timer = false;
    }

    public void Close(){
        if(canClick){
            Debug.Log("Close");
            StartCoroutine(Down());
        }
    }

    public void Up(string text){
        this.gameObject.SetActive(true);
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        tmp.text = text;
    }

    public IEnumerator Up(){
        while(Vector3.SqrMagnitude(transform.localPosition - upPosition) >= 0.001f){
            transform.localPosition = Vector3.Lerp(transform.localPosition, upPosition, speed * Time.deltaTime);
            yield return null;
        }
        transform.localPosition = upPosition;
        canClick = true;
    }

    public IEnumerator Down(){
        while(Vector3.SqrMagnitude(transform.localPosition - downPosition) >= 0.001f){
            transform.localPosition = Vector3.Lerp(transform.localPosition, downPosition, speed * Time.deltaTime);
            yield return null;
        }
        transform.localPosition = downPosition;
        this.gameObject.SetActive(false);
        Timer.instance.increase_timer = true;
        canClick = false;
    }

}
