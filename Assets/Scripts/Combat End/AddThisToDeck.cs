using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddThisToDeck : MonoBehaviour
{
    public CombatManager Manager;
    public GameObject ThisCard;

    public Deck Deck;

    public GameObject OtherCard1, OtherCard2;

    private void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Deck.AddCardToDeck(ThisCard);

            Manager.Continue();

            Destroy(OtherCard1);
            Destroy(OtherCard2);
            Destroy(gameObject);
            
        }

    }
}
