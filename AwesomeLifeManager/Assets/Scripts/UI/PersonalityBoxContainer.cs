using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalityBoxContainer : MonoBehaviour
{
    public static PersonalityBoxContainer instance;

    int pibot = 0;

    public void Start(){
        instance = this;
    }

    public void AddBox(GameObject p_box){
        RectTransform t_rect = (RectTransform)p_box.transform;

        int t_x = pibot/2;
        int t_y = pibot%2;

        t_rect.localPosition = new Vector2(t_rect.rect.width * t_y,
                                            -t_rect.rect.height * t_x);
        pibot ++;
    }
}
