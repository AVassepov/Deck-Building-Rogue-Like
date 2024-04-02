using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardCard : MonoBehaviour
{
    public List<GameObject> Cards;
    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < Cards.Count; i++)
        {

            if (Mathf.Abs(Cards[i].transform.position.x - transform.position.x) < 0.15f)
            {
                Destroy(Cards[i]);
                Cards.RemoveAt(i);
            }
            else
            {
                MoveCardToDiscard(i, Cards[i]);
            }


        }

    }


    private void MoveCardToDiscard(int i, GameObject currentCard)
    {
        currentCard.transform.position = Vector3.Lerp(currentCard.transform.position, transform.position, 0.05f);

    }

    public void Discard(GameObject card)
    {
        Cards.Add(card);

    }
}
