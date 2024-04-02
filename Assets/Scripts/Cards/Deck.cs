using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{


    [Header("Lists")]

    public List<GameObject> DeckOfCards;
    public List<GameObject> Discarded;
    public List<GameObject> Hand;
    public List<Transform> CardPositions;
    public List<GameObject> currentHand;
    public List<GameObject> Exhausted;
    public List<GameObject> TotalCards;



    private int currentCardSlot;


    [Header("Id and bools")]
    public int Player;
    public bool NotAimingAtHand;

    private SkillsPlayArea SkillsPlayArea;

    private int cardsLeft;
    [Header("Managers")]
    private StatusEffects statusEffects;

    private bool drawingCards;

    public DisplayDeck Display;

    [HideInInspector]
    public  DiscardCard discardCards;

    private void Awake()
    {

        Discarded = new List<GameObject>(TotalCards);

        ShuffleDeck();
        SkillsPlayArea = GameObject.Find("Managers").GetComponent<SkillsPlayArea>();
        statusEffects = gameObject.GetComponent<StatusEffects>();
        discardCards = GameObject.Find("Discard Cards").GetComponent<DiscardCard>();


        for (int i = 0; i < TotalCards.Count; i++)
        {
            statusEffects.totalDeckRarity += TotalCards[i].GetComponent<Card>().CardInfo.Rarity;

        }


    }

    public void Update()
    {
        NotAimingAtHand = SkillsPlayArea.NotAimingAtHand;

            for (int i = 0; i < currentHand.Count; i++)
            {
            if (!currentHand[i].GetComponent<Card>().ChosenToDiscard) { MoveCard(i, currentHand[i]); }
            }

    }


    public void DrawCards()
    {
        DiscardHand();

        if (Hand.Count == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                // make sure there are is a card to draw , before drawing a card
                if(DeckOfCards.Count>0) { 
                GameObject currentCard;
                //currentCard = Instantiate(DeckOfCards[DeckOfCards.Count-1], CardPositions[i]);
                currentCard = Instantiate(DeckOfCards[DeckOfCards.Count - 1], transform.position, Quaternion.identity);
                currentCard.GetComponent<Card>().PositionInHand = i;
                currentCard.GetComponent<ApplyDataToCard>().defaultLayer = i;
                currentHand.Add(currentCard);
                Hand.Add(DeckOfCards[DeckOfCards.Count - 1]);
                DeckOfCards.RemoveAt(DeckOfCards.Count - 1);
                }
            }
            currentCardSlot = 5;

        }

    }


    public void DiscardHand()
    {

        while (Hand.Count > 0)
        {

               Discarded.Add(Hand[0]);
                  Hand.RemoveAt(0);
            //Destroy(currentHand[0]);
            discardCards.Cards.Add(currentHand[0]);
                 currentHand.RemoveAt(0);

        }

        currentCardSlot = 0;

    }

    public void DiscardRandom()
    {
        if(currentHand.Count > 0)
        {
            int index = Random.Range(0, Hand.Count);

            Discarded.Add(Hand[index]);
            Hand.RemoveAt(index);
            //  Destroy(currentHand[index]);
            discardCards.Cards.Add(currentHand[index]);
            currentHand.RemoveAt(index);
            currentCardSlot--;

        }


    } public void UpdatePositions()
        {
        for (int i = 0; i < Hand.Count; i++)
        {
            currentHand[i].GetComponent<Card>().PositionInHand = i;
            currentHand[i].GetComponent<ApplyDataToCard>().defaultLayer = i;

        }

        for (int i = 0;i < Hand.Count;i++) {
            if (i < 11)
            {
                currentHand[i].transform.parent = CardPositions[i].transform;

            }
       }

        currentCardSlot--;
    }

    public void ShuffleDeck()
    {
        cardsLeft = DeckOfCards.Count;


        while (Discarded.Count > 0) { 
            int index = Random.Range(0, Discarded.Count);
            GameObject temp = Discarded[index];
            DeckOfCards.Add(temp);
            Discarded.RemoveAt(index);

        }

        if (cardsLeft > 0)
        {
            for (int i = 0; i < cardsLeft; i++)
            {
                DeckOfCards.Add(DeckOfCards[0]);
                DeckOfCards.RemoveAt(0);
            }
        }
    }
    

    public void DrawCard()
    {
        GameObject currentCard;

        //if more than maximum discard that card instead
       if(currentHand.Count==10) {
            if(DeckOfCards.Count>0) { 
            currentCard = Instantiate(DeckOfCards[DeckOfCards.Count - 1], transform);
            currentCard.GetComponent<Card>().PositionInHand = currentCardSlot+2;
            currentCard.GetComponent<ApplyDataToCard>().defaultLayer = currentCardSlot;
            discardCards.Cards.Add(currentCard);
            }

        }
        else{
            // if deck is empty, shuffle 
            if (DeckOfCards.Count == 0)
            {
                ShuffleDeck();
            }
            //if still empty after shuffling, dont do anything , but will actually draw same card unless it was exhausted 
            // make sure there are is a card to draw , before drawing a card
            if (DeckOfCards.Count > 0)
            {

                currentCardSlot++;
                currentCard = Instantiate(DeckOfCards[DeckOfCards.Count - 1], transform);
                currentCard.GetComponent<Card>().PositionInHand = currentCardSlot;
                currentCard.GetComponent<ApplyDataToCard>().defaultLayer = currentCardSlot;
                currentHand.Add(currentCard);

                print("Drew: " + currentCard.GetComponent<Card>().CardInfo.Name);

                Hand.Add(DeckOfCards[DeckOfCards.Count - 1]);
                DeckOfCards.RemoveAt(DeckOfCards.Count - 1);

            }
        }
    }

    public void Exhaust(GameObject exhausted)
    {
        Exhausted.Add(Discarded[Discarded.Count-1]);
        Discarded.RemoveAt(Discarded.Count-1);

    }

    public void MoveCard(int i, GameObject currentCard)
    {
            currentCard.transform.position = Vector3.Lerp(currentCard.transform.position, CardPositions[i].position, 0.03f);
          
    }
    public void DiscardSelected(int PositionInHand)
    {

        Discarded.Add(Hand[PositionInHand]);
        Hand.RemoveAt(PositionInHand);
        //  Destroy(currentHand[index]);
        discardCards.Cards.Add(currentHand[PositionInHand]);
        currentHand.RemoveAt(PositionInHand);
        currentCardSlot--;

    }



    public void AddCardToDeck(GameObject AddedCard)
    {
        TotalCards.Add(AddedCard);
        Display.UpdateCards();
    }
}
