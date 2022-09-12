using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanerReveal : MonoBehaviour
{
    [SerializeField] PlanerUI planerUI;
    [SerializeField] RectTransform UI;
    [SerializeField] Mask mask;
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] Animator cameraAnime;
    bool toggle = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click(){
        if(toggle){
            StartCoroutine(SizeCo());
            toggle = !toggle;
        }
        else{
            toggle = !toggle;
        }
    }
    IEnumerator SizeCo(){
        tmp.enabled = false;
        mask.enabled = false;
        RectTransform t_rect = GetComponent<RectTransform>();
        while(Vector2.Distance(t_rect.sizeDelta,UI.sizeDelta + new Vector2(200,200)) >= 1){
            t_rect.sizeDelta = Vector2.Lerp(t_rect.sizeDelta,
                                            UI.sizeDelta + new Vector2(200,200),
                                            6f * Time.deltaTime);
            yield return null;
        }
        UI.gameObject.SetActive(false);
        cameraAnime.SetTrigger("walking");
        planerUI.gameObject.SetActive(true);
    }
}
