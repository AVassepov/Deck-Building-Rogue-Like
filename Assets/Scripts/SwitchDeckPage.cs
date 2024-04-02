using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDeckPage : MonoBehaviour
{
    [SerializeField] DisplayDeck displayDeck;
    [SerializeField] int Value;
    private void OnMouseOver()
    {
       if (Input.GetMouseButtonDown(0))
        {
            displayDeck.ChangePage(Value);

        }
    }

}
