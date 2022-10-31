using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimeHandler : MonoBehaviour
{
    [SerializeField] EventPopup eventPopup;

    public void OnEndEncounterAnime()
    {
            eventPopup.eventEncounterAnime.gameObject.SetActive(false);
            eventPopup.Container.SetActive(true);
    }
}
