using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ActionMenu : UI
{
    public ActionReveal revealButton;
    public ActionButton[] actionButtons;
    public GameObject slotContainer;
    public GameObject Screen;
    public Slot[] slots;
    public RectTransform origin;
    
    Vector2 tracePos;
    Quaternion destRot;
    int raidus = 185;
    float SwipeMinDis;
    public bool keyDown = false;
    float currentSpeed = 0;

    private double ceta;
    private float trace_distance = 0;
    private bool snapped = false;
    private Dictionary<int,int> key_maps = new Dictionary<int,int>();

    void Awake(){
        key_maps.Add(1,0);
        key_maps.Add(2,5);
        key_maps.Add(3,4);
        key_maps.Add(4,3);
        key_maps.Add(5,2);
        key_maps.Add(0,1);
    }

    void OnEnable()
    {
        Screen.SetActive(true);
        Vector2 t_origin = origin.localPosition;
        Debug.Log(t_origin);
        Vector2[] poses = Utility.makeCircle(t_origin, actionButtons.Length, raidus, ref ceta);
        for(int i = 0; i < actionButtons.Length; i ++){
            actionButtons[i].revPos = t_origin + poses[i];
            actionButtons[i].Reveal();
            slots[i].gameObject.transform.localPosition = t_origin + poses[i];
            actionButtons[i].disPos = t_origin;
        }
        Quaternion quaternion = Quaternion.identity;
        quaternion.eulerAngles = new Vector3(0,0,90);
        this.transform.rotation = quaternion;
        slotContainer.transform.rotation = quaternion;
    }

    void OnDisable(){
        Screen.SetActive(false);
    }

    public void SetActive(bool p_bool){
        if(!revealButton.canPressButton){
            if(p_bool){
                this.gameObject.SetActive(p_bool);
                UIManager.instance.canSwipe = false;
            }
            else{
                Vector2 t_origin = origin.anchoredPosition;
                for(int i = 0; i < actionButtons.Length; i ++){
                    actionButtons[i].gameObject.SetActive(true);
                    actionButtons[i].Scaling(1f);
                    actionButtons[i].Dissap();
                    slots[i].gameObject.transform.localPosition = t_origin;
                    UIManager.instance.canSwipe = true;
                }
            }
        }
    }

    public override bool onClickDown(Vector2 pos){
        keyDown = true;
        return true;
    }

    public override bool onClickUp(float f, Vector2 pos){
        keyDown = false;
        snap();
        trace_distance = 0;
        return true;
    }

    public override bool onSwipe(Vector2 sp, Vector2 ep){
        currentSpeed = Vector2.Distance(sp, ep);
        float dis = sp.x - ep.x;
        Debug.Log(trace_distance);
        if( currentSpeed != 0 && Math.Abs(trace_distance + dis) < 360){
            trace_distance += dis;
            Quaternion quaternion = Quaternion.identity;
            quaternion.eulerAngles = new Vector3(0, 0, dis);
            this.transform.rotation = this.transform.rotation * quaternion;
            SettingButton();
        }
        return true;
    }

    private void snap(){
        int t_num = (int)(this.gameObject.transform.rotation.eulerAngles.z/(ceta *  180d / Math.PI));
        double snap_radian = ((t_num -1) * ceta)+(Math.PI/2);
        Quaternion quaternion = Quaternion.identity;
        quaternion.eulerAngles = new Vector3(0, 0, (float)(snap_radian * 180d / Math.PI));
        destRot = quaternion;
        StartCoroutine(SpinCo());
    }

    public void SettingButton(){
        for(int i = 0; i < actionButtons.Length; i ++){
            int t_slot = get_slot(actionButtons[i]);
            actionButtons[i].Scaling(slots[t_slot].contentSize);
        }
    }

    private int get_slot(ActionButton p_button){
        int r = 0;
        for(int i = 0; i < slots.Length; i ++){
            if(Vector2.Distance(p_button.transform.position, slots[i].transform.position) 
                < Vector2.Distance(p_button.transform.position, slots[r].transform.position))
                    r = i;
        }
        return r;
    }

    IEnumerator SpinCo(){
        while(Quaternion.Angle(this.transform.rotation, destRot) > 0.5f){
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, destRot, 50 * Time.deltaTime);
            yield return null;
        }
        this.transform.rotation = destRot;
        SettingButton();
    }
}
