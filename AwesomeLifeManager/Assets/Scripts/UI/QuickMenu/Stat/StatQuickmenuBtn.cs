using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatQuickmenuBtn : QuickMenuButton
{
    public override void OnExitQuickmenu()
    {
        StatChangeButton.activatedButton.Deactive(false);
    }

    public override void OnClickQuickmenu()
    {
        StatChangeButton[] t_btns = this.activationObj.GetComponentsInChildren<StatChangeButton>();
        foreach(StatChangeButton b in t_btns)
        {
            if (b.setActivatedButtonThis)
            {
                b.Active(false);
                StatChangeButton.activatedButton = b;
                break;
            }
        }
    }
}
