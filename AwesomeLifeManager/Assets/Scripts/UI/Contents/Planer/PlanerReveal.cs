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
    UIManager theUIManager;
    bool toggle = true;
    // Start is called before the first frame update
    void Start()
    {
        container = GetComponentInParent<ContentsContainer>();
        theUIManager = UIManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnClick(){
        if(toggle){
            theUIManager.externalListenerFired = true;
            Container t_con = GetComponentInParent<Container>();
            t_con.ZoomIn(true);
            StartCoroutine(container.SnapCo(this.GetComponent<RectTransform>(),() => { StartCoroutine(SizeCo()); }));
        }
    }

    public override void OnStartCoroutine()
    {
        toggle = false;
        tmp.enabled = false;
    }

    public override void OnEndCoroutine()
    {
        toggle = true;
        //planerUI.gameObject.SetActive(true);
        tmp.enabled = true;
        base.contentsBoxPopup.SetActive(true);
        theUIManager.uiEnabled = false;
        //UI.gameObject.SetActive(false);
    }
}
