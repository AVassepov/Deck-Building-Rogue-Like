using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

public class ShowCardPiles : MonoBehaviour
{
    private List<GameObject> CurrentPile;

    [SerializeField] private bool IsDiscardPile;

    private TurnManager TurnManager;


    private List<GameObject> AllCards;

    public int CurrentPage;


    public List<GameObject> CurrentCards;


    private SortingGroup SortingGroup;

    [SerializeField] GameObject background;
    [SerializeField] GameObject PageUp;
    [SerializeField] GameObject PageDown;
    [SerializeField] GameObject Close;    

    private void Start()
    {
        TurnManager= GameObject.Find("Managers").GetComponent<TurnManager>();
    }



    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {

            SetCurrentPile();
             DisplayPage();
            GameObject BackGround;

           BackGround= Instantiate(background, new Vector3(0.0299999993f, 3.25999999f, -0.850000024f) , Quaternion.identity);
            GameObject up, down , close;

                up=Instantiate(PageUp);
                down=Instantiate(PageDown);
                close = Instantiate(Close);

            CloseDeck closeDeck;
            closeDeck= close.GetComponent<CloseDeck>();
            closeDeck.Up=up;
            closeDeck.Down=down;
            closeDeck.BackGround = BackGround;
            closeDeck.CardPiles = this;
            closeDeck.Deck = CurrentCards;

           
            up.GetComponent<SwitchDiscardOrDeckPage>().ShowPile = this;
            down.GetComponent<SwitchDiscardOrDeckPage>().ShowPile = this;
        }

    }

    private void SetCurrentPile()
    {
        if (IsDiscardPile)
        {
            CurrentPile = new List<GameObject>(TurnManager.CurrentDeck.Discarded);
            AllCards = CurrentPile;
        }
        else
        {
            CurrentPile = new List<GameObject>(TurnManager.CurrentDeck.DeckOfCards);
            AllCards = CurrentPile;
        }
    }


    private void DisplayPage()
    {
        bool Continue = true;
        int index = 0 + (15 * CurrentPage);
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Continue)
                {
                    if (index < AllCards.Count)
                    {
                        GameObject currentCard;
                        currentCard = Instantiate(AllCards[index], new Vector3(transform.position.x + (i * 2.2f) + 0.5f, transform.position.y + (j * 3) + 0.5f, transform.position.z), Quaternion.identity);
                        currentCard.transform.parent = transform;
                        currentCard.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                        CurrentCards.Add(currentCard);
                        Destroy(currentCard.GetComponent<Card>());
                        currentCard.AddComponent<TopSortingLayer>();
                    }
                }

                //if the page is not going to be completely filled, stop cycling
                if (index < AllCards.Count - 1)
                {
                    index++;
                }
                else
                {
                    Continue = false;
                    break;
                }


                //if next page is empty, dont move to it
                if (CurrentCards.Count < 1)
                {
                    ChangePage(-1);
                    print("nothing to see here");
                }


            }
        }

        print(CurrentCards.Count);
        //if next page is empty, dont move to it
        if (CurrentCards.Count < 1)
        {
            ChangePage(-1);
            print("nothing to see here");
        }


    }

    public void ChangePage(int number)
    {

        if ((number == -1 && CurrentPage > 0) || (number == 1 && CurrentPage < AllCards.Count / 15))
        {
            CurrentPage += number;
            DestroyPreviousPage();
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




}
