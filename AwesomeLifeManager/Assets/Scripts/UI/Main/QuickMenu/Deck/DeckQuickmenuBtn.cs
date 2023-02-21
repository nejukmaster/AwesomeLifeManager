using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckQuickmenuBtn : QuickMenuButton
{
    [SerializeField] DeckPreview deckPreview;
    public override void OnExitQuickmenu()
    {
        CategoryChangeButton.activatedButton.Deactive();
    }
    
    public override void OnClickQuickmenu()
    {
        CategoryChangeButton[] t_btn = this.activationObj.GetComponentsInChildren<CategoryChangeButton>();
        foreach(CategoryChangeButton b in t_btn)
        {
            if (b.isActivated)
            {
                b.Active();
                CategoryChangeButton.activatedButton = b;
                break;
            }
        }
        deckPreview.UpdatePreview();
    }
}
