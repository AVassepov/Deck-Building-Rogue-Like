using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCards : MonoBehaviour
{
    public Deck savedDeck;
    public int i;
    public ApplyDataToCard CardUsed;
    public GameObject Saved;
    public Rest rest;

    public GameObject Card1, Card2, Arrow;

    public void ConfirmUpgrade()
    {

        //Confirm upgrade
        /*  if (savedDeck.TotalCards[i].GetComponent<ApplyDataToCard>().CardInfo.Name == CardUsed.CardInfo.Name)
          {*/
            print("Found Card");
        if(savedDeck != null && Saved!=null) {
            savedDeck.TotalCards[i] = Saved;
        }
        //}

        // switching active player for later
        if (rest != null &&rest.CurrentPlayer == rest.Player1)
        {
            rest.CurrentPlayer = rest.Player2;
        }
        else
        {
            FindObjectOfType<DungeonNavigation>().CanMove = true;
            FindObjectOfType<EncounterManager>().CurrentRoom.ClearRoom();
            if(rest!=null)
            {
                Destroy(rest.gameObject);

            }
        }


        if(Card1 && Card2 && Arrow && rest) { 
        Destroy(Card1 );
        Destroy(Card2 );
        Destroy(Arrow );
        Destroy(rest.UpgradeUIInstance);
        Destroy(gameObject);
        }
    }



    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ConfirmUpgrade();
        }
    }


}
