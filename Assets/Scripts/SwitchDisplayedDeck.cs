using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDisplayedDeck : MonoBehaviour
{
    [SerializeField] DisplayDeck displayDeck;
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            displayDeck.ChangeDeck();

        }
    }
}
