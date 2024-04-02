using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DisplayDeck : MonoBehaviour
{
    public Deck deck1;
    public Deck deck2;

    private Deck currentDeck;

    private List<GameObject> AllCards;

    public int CurrentPage;

    [SerializeField] private List<GameObject> UnupgradedCardsOnly;

    public List<GameObject> CurrentCards;

    public bool isUpgrading;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCards();
    }




    private void DisplayPage()
    {
        bool Continue= true;
            int index = 0 + (15 * CurrentPage);
        List<GameObject> ListOfCards;
        if(isUpgrading )
        {
            for(int i = 0; i < AllCards.Count; i++)
            {
                if (!AllCards[i].name.Contains("+"))
                {
                    UnupgradedCardsOnly.Add(AllCards[i]);
                }
            }

            ListOfCards = UnupgradedCardsOnly;
        }
        else
        {
            ListOfCards = AllCards;
        }




            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Continue)
                    {
                        if (index < ListOfCards.Count)
                        {
                            GameObject currentCard;
                            currentCard = Instantiate(ListOfCards[index], new Vector3(transform.position.x + (i * 2.2f) + 0.5f, transform.position.y + (j * 3) + 0.5f, transform.position.z - 0.4f), Quaternion.identity); ;
                            currentCard.transform.parent = transform;
                            currentCard.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                            CurrentCards.Add(currentCard);
                            if (isUpgrading)
                            {
                                currentCard.GetComponent<ApplyDataToCard>().ResetLayerForUpgrades();
                                currentCard.GetComponent<ApplyDataToCard>().isUpgrading = true;
                                currentCard.AddComponent<UpgradeCards>();
                            }
                            Destroy(currentCard.GetComponent<Card>());
                        }
                    }

                    //if the page is not going to be completely filled, stop cycling
                    if (index < ListOfCards.Count - 1)
                    {
                        index++;
                    }
                    else
                    {
                        Continue = false;
                        break;
                    }
                }

             }
        //if next page is empty, dont move to it
        if (CurrentCards.Count < 1)
        {
            ChangePage(-1);
            print("nothing to see here");
        }

    }

    public void ChangePage(int number)
    {

        if((number == -1 && CurrentPage > 0) || (number ==1 && CurrentPage < AllCards.Count/15))
        {
            CurrentPage += number;
            DestroyPreviousPage();
            DisplayPage();
        }
    }

    public void ChangeDeck()
    {
        if(currentDeck == deck1)
        {
            currentDeck = deck2;
            AllCards = currentDeck.TotalCards;
            DestroyPreviousPage();
            CurrentPage = 0;
            DisplayPage();
        }
        else
        {
            currentDeck = deck1;
            AllCards = currentDeck.TotalCards;
            DestroyPreviousPage();
            CurrentPage = 0;
            DisplayPage();
        }

    }


    private void DestroyPreviousPage()
    {
        for (int i = 0; i < CurrentCards.Count; i++)
        {
            Destroy(CurrentCards[i]);
        }
        CurrentCards.Clear();

    }



    public void UpdateCards()
    {

        currentDeck = deck1;

        if (currentDeck) { 
        AllCards = currentDeck.TotalCards;
        }

        DisplayPage();

    }



}
