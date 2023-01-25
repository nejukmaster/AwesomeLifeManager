using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsBoxPopup : MonoBehaviour
{
    public void SetActive(bool p_bool, GameObject p_obj)
    {
        this.gameObject.SetActive(p_bool);
        if (p_obj != null)
        {
            p_obj.SetActive(p_bool);
        }
    }
}
