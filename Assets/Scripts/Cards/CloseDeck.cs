using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CloseDeck : MonoBehaviour
{
    public GameObject BackGround;
    public GameObject Up;
    public GameObject Down;

    public ShowCardPiles CardPiles;

    public List<GameObject> Deck;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < Deck.Count; i++)
            {
                Destroy(Deck[i]);

            }
            CardPiles.CurrentPage = 0;
            Destroy(Up);
            Destroy(Down);
            Destroy(BackGround);
            Destroy(gameObject);

        }

    }
}
