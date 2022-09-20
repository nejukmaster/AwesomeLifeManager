using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanerReveal : ContentsBox
{
    [SerializeField] PlanerUI planerUI;
    [SerializeField] RectTransform UI;
    [SerializeField] Mask mask;
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] Animator cameraAnime;

    ContentsContainer container;
    bool toggle = true;
    // Start is called before the first frame update
    void Start()
    {
        container = GetComponentInParent<ContentsContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click(){
        if(toggle){
            StartCoroutine(container.SnapCo(this.GetComponent<RectTransform>(),() => { StartCoroutine(SizeCo()); }));
            toggle = !toggle;
        }
        else{
            toggle = !toggle;
        }
    }

    public override void OnStartCoroutine()
    {
        tmp.enabled = false;
        mask.enabled = false;
    }

    public override void OnEndCoroutine()
    {
        planerUI.gameObject.SetActive(true);
        tmp.enabled = true;
        mask.enabled = true;
        UI.gameObject.SetActive(false);
    }
}
