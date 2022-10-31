using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventPopup : MonoBehaviour
{
    public Animator eventEncounterAnime;
    public TextMeshProUGUI name;
    public Image eventIllustration;
    public TextMeshProUGUI[] choices;
    public GameObject Container;
    
    public void EventEncounter()
    {
        eventEncounterAnime.SetTrigger("encounter");
    }
}
