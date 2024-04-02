using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ApplyEventData : MonoBehaviour
{
    public Events Event;

    [Header("Componenets")]
    [SerializeField] private TextMesh Title;
    [SerializeField] private SpriteRenderer Image;
    [SerializeField] private TextMesh Description;

    [SerializeField] private GameObject[] Options;

    private void Start()
    {
        Title.text = Event.Title;
        Image.sprite = Event.Image;
        Description.text = Event.Description;

        if (Event.OptionOutcomes.Length == 2)
        {
            Destroy(Options[2]);

            Options[0].GetComponentInChildren<TextMesh>().text = Event.OptionNames[0];
            Options[1].GetComponentInChildren<TextMesh>().text = Event.OptionNames[1];

            Options[0].GetComponent<EventButtons>().SavedEvent = Event;
            Options[1].GetComponent<EventButtons>().SavedEvent = Event;
        }
        else
        {

            Options[0].GetComponentInChildren<TextMesh>().text = Event.OptionNames[0];
            Options[1].GetComponentInChildren<TextMesh>().text = Event.OptionNames[1];
            Options[2].GetComponentInChildren<TextMesh>().text = Event.OptionNames[2];

            Options[0].GetComponent<EventButtons>().SavedEvent = Event;
            Options[1].GetComponent<EventButtons>().SavedEvent = Event;
            Options[2].GetComponent<EventButtons>().SavedEvent = Event;
        }
        AppplyOutcomesToButtons();
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    void AppplyOutcomesToButtons()
    {

        for (int i = 0; i < Event.OptionNames.Length; i++)
        {
            if (Event.OptionOutcomes[i] == Events.options.Leave)
            {
                Options[i].GetComponent<EventButtons>().outcome = 0;
            }
            else if (Event.OptionOutcomes[i] == Events.options.Fight)
            {
                Options[i].GetComponent<EventButtons>().outcome = 1;
            }
            else if (Event.OptionOutcomes[i] == Events.options.GetItem)
            {
                Options[i].GetComponent<EventButtons>().outcome = 2;
            }
            else if (Event.OptionOutcomes[i] == Events.options.GetResource)
            {
                Options[i].GetComponent<EventButtons>().outcome = 3;
            }
            else if (Event.OptionOutcomes[i] == Events.options.WildCard)
            {
                Options[i].GetComponent<EventButtons>().outcome = 4;
            }

        }
    }
}
