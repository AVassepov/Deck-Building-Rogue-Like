using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDiscardManager : MonoBehaviour
{
    public int CardsToDiscard = 1;
    public int CurrentlyDiscarding;
    public Deck DeckUsed;

    public List<Card> DiscardList;

    // Start is called before the first frame update
    void Start()
    {
        if(CardsToDiscard>= DeckUsed.currentHand.Count)
        {
            DeckUsed.DiscardHand();
            Destroy(transform.parent.gameObject);
            FindObjectOfType<TurnManager>().CanContinue = true;
        }
   
    }

    // Update is called once per frame
    void Update()
    {
        if (DeckUsed.currentHand.Count == 0)
        {
            print("Nothing to discard");
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(CurrentlyDiscarding == CardsToDiscard)
            {
                DiscardSelected();
                ReleaseAllOtherCards();
                Destroy(transform.parent.gameObject);
                FindObjectOfType<TurnManager>().CanContinue = true;
            }
            else
            {
                print("Not Enough Selected");
            }
        }
    }


    private void DiscardSelected()
    {

        int counter = 0;
        for (int i = 0; i < DiscardList.Count; i++)
        {
            DiscardList[i].currentDeck.DiscardSelected(DiscardList[i].PositionInHand- counter);
            counter++;
            DiscardList[i].DiscarderPosition = GameObject.Find("UI").transform.GetChild(0).transform.position;
        }
    }

    private void ReleaseAllOtherCards()
    {

        for (int i = 0; i < DeckUsed.currentHand.Count; i++)
        {
            DeckUsed.currentHand[i].GetComponent<Card>().isDiscarding = false;
            DeckUsed.currentHand[i].GetComponent<Card>().ChosenToDiscard = false;
            DeckUsed.UpdatePositions();
        }
    }

}
