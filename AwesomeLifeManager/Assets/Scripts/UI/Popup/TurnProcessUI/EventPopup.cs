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
    public Event currentEvent;
    
    public void SetActive(bool p_bool, Event p_event)
    {
        this.gameObject.SetActive(p_bool);
        currentEvent = p_event;
        name.text = p_event.name;
        if(p_event.mainTex != null)
            eventIllustration.sprite = p_event.mainTex;
        for (int i = 0; i < choices.Length; i++)
            choices[i].text = p_event.choices[i].name;
    }
    public void EventEncounter()
    {
        eventEncounterAnime.SetTrigger("encounter");
    }
}
