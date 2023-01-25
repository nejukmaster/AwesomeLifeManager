using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventPopup : MonoBehaviour
{
    public Animator eventEncounterAnime;
    public TurnProcessPopup turnProcessPopup;
    public TextMeshProUGUI name;
    public Image eventIllustration;
    public GameObject[] choices;
    public GameObject Container;
    public Event currentEvent;

    public void SetActive(bool p_bool, Event p_event)
    {
        this.gameObject.SetActive(p_bool);
        if (!p_bool)
        {
            for (int i = 0; i < choices.Length; i++)
                choices[i].GetComponent<EventChoiceButton>().Initialize();
            UI.ToggleSubUI(turnProcessPopup.gameObject, true);
        }
        else
        {
            eventEncounterAnime.gameObject.SetActive(true);
            UI.ToggleSubUI(turnProcessPopup.gameObject, false);
            currentEvent = p_event;
            name.text = p_event.name;
            if (p_event.mainTex != null)
                eventIllustration.sprite = p_event.mainTex;
            for (int i = 0; i < choices.Length; i++)
            {
                choices[i].gameObject.SetActive(true);
                choices[i].GetComponentInChildren<TextMeshProUGUI>().text = p_event.choices[i].name;
            }
        }
    }
    public void EventEncounter()
    {
        eventEncounterAnime.SetTrigger("encounter");
    }
}
