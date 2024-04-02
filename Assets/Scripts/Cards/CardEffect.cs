using UnityEngine;

public class CardEffect : MonoBehaviour
{

    public GameObject DamageTextInstance;


    private Card card;
    private CardInfo cardInfoThis;
    private Deck deck;
    private DiscardCard Discard;
    private StatusEffects statusEffects;
    private PlayerHealth health;
    private ApplyDataToCard description;
    private void Awake()
    {
        card = gameObject.GetComponent<Card>();

        if(card!=null)
        {
            cardInfoThis = card.CardInfo;
            deck = card.currentDeck;
            statusEffects = card.statusEffects;
            health = card.turnManager.CurrentHealth;
        }
       
        description = gameObject.GetComponent<ApplyDataToCard>();

      

    }

    private void Start()
    {
        if (deck != null)
        {
            Discard = deck.discardCards;
        }
        ApplyAdditionalText();
    }

    public void PlayCard()
    {
        // Rapier Strike
        if (cardInfoThis.Name == "Rapier Strike" || cardInfoThis.Name == "Rapier Strike+") { 
            card.RemoveCardFromHand();

            //Remove energy and deal damage
            card.EnemyHealth.CurrentHealth -= (cardInfoThis.DamageOrBlock + statusEffects.Strength );
            card.Energy.CurrentEnergy -= cardInfoThis.EnergyCost;


            DamageText((cardInfoThis.DamageOrBlock + statusEffects.Strength).ToString());


            //Fix the deck
            FixTheDeck();
        }


        // Block
        if (cardInfoThis.Name == "Block" || cardInfoThis.Name == "Block+")
        {
            card.RemoveCardFromHand();

            //Remove energy and deal damage
            card.CurrentPlayerHealth.GainBlock(cardInfoThis.DamageOrBlock);
            card.Energy.CurrentEnergy -= cardInfoThis.EnergyCost;


            //Fix the deck
            FixTheDeck();
        }

        // Coin Flip 
        if (cardInfoThis.Name == "Coin Flip" || cardInfoThis.Name ==  "Coin Flip+")
        {
            card.RemoveCardFromHand();

        //Remove energy and deal damage or Give block
        int random = Random.Range(1, 3);


            card.Energy.CurrentEnergy -= cardInfoThis.EnergyCost;

            if (random == 1)
            {
                card.EnemyHealth.CurrentHealth -= (cardInfoThis.DamageOrBlock + statusEffects.Strength );

                DamageText((cardInfoThis.DamageOrBlock + statusEffects.Strength).ToString());

            }
            else if (random == 2) 
            {
                health.GainBlock(cardInfoThis.DamageOrBlock);

            }
            print(random);

            //Fix the deck
            FixTheDeck();
        }

        // Inner Voices 
        if (cardInfoThis.Name == "Inner Voices" || cardInfoThis.Name == "Inner Voices+")
        {
            card.RemoveCardFromHand();

            //Remove energy and deal damage
            card.EnemyHealth.CurrentHealth -= (cardInfoThis.DamageOrBlock + statusEffects.Strength);
            card.Energy.CurrentEnergy -= cardInfoThis.EnergyCost;


            //card.Deck.DiscardRandom();
            deck.DiscardRandom();
            deck.DrawCard();

            if (cardInfoThis.Name == "Inner Voices+")
            {
                deck.DrawCard();
            }

            //Grant Instanity Effect
            statusEffects.Instanity++;



            DamageText((cardInfoThis.DamageOrBlock + statusEffects.Strength).ToString());

            //exhaust
            deck.Exhaust(gameObject);


            //Fix the deck
            FixTheDeck();
        }

        // Truce 
        if (cardInfoThis.Name == "Truce" || cardInfoThis.Name == "Truce+")
        {
            card.RemoveCardFromHand();

            //Remove energy
            card.Energy.CurrentEnergy -= cardInfoThis.EnergyCost;


            card.EnemyHealth.gameObject.GetComponent<EnemyActions>().CanAttack = false;
            statusEffects.CanAttackStacks++;
            deck.Exhaust(gameObject);

            //Grant Instanity Effect
            print("Truce");


            //Fix the deck
            FixTheDeck();
        }


        // Riposte
        if (cardInfoThis.Name == "Riposte" || cardInfoThis.Name == "Riposte+")
        {
            card.RemoveCardFromHand();

            //Remove energy and deal damage
            statusEffects.Riposting = true;
            statusEffects.RipostingDamage += cardInfoThis.DamageOrBlock;

            card.Energy.CurrentEnergy -= cardInfoThis.EnergyCost;

            deck.Exhaust(gameObject);


            //Fix the deck
            FixTheDeck();
        }


        // Stoicism 
        if (cardInfoThis.Name == "Stoicism" || cardInfoThis.Name == "Stoicism+")
        {
            card.RemoveCardFromHand();

            //Remove energy
            card.Energy.CurrentEnergy -= cardInfoThis.EnergyCost;


            if (cardInfoThis.Name == "Stoicism" && health.CurrentHP > health.MaxHP / 2)
            {
                health.Heal(cardInfoThis.DamageOrBlock);
            } else if (cardInfoThis.Name == "Stoicism+" && health.CurrentHP > (health.MaxHP / 4)*3)
            {
                health.Heal(cardInfoThis.DamageOrBlock);
            }
            else
            {
                health.Heal(cardInfoThis.DamageOrBlock);
                health.Heal(cardInfoThis.DamageOrBlock);
            }
            print("Stoicism");



            //Fix the deck
            FixTheDeck();
        }


        //Big Sword  
        if (cardInfoThis.Name == "Big Sword" || cardInfoThis.Name == "Big Sword+")
        {
            card.RemoveCardFromHand();

            //Remove energy and deal damage
            if (cardInfoThis.Name == "Big Sword")
            {
                card.EnemyHealth.CurrentHealth -= (cardInfoThis.DamageOrBlock + statusEffects.Strength * 2);
            }
            else
            {
                card.EnemyHealth.CurrentHealth -= (cardInfoThis.DamageOrBlock + statusEffects.Strength * 3);
            }
            card.Energy.CurrentEnergy -= cardInfoThis.EnergyCost;



            DamageText((cardInfoThis.DamageOrBlock + statusEffects.Strength * 2).ToString()) ;

            //Fix the deck
            FixTheDeck();
        }



        // Psychosis
        if (cardInfoThis.Name == "Psychosis" || cardInfoThis.Name ==  "Psychosis+")
        {
            card.RemoveCardFromHand();

            //Remove energy and deal damage
            statusEffects.Instanity =+ 3;
            health.GainBlock(cardInfoThis.DamageOrBlock);

            card.Energy.CurrentEnergy -= cardInfoThis.EnergyCost;

            deck.Exhaust(gameObject);


            //Fix the deck
            FixTheDeck();
        }



        // Face Your Fears
        if (cardInfoThis.Name == "Face Your Fears" || cardInfoThis.Name == "Face Your Fears+")
        {
            card.RemoveCardFromHand();

            //Remove energy and deal damage
            statusEffects.PanicStacks += cardInfoThis.DamageOrBlock;
            statusEffects.FearStacks += cardInfoThis.DamageOrBlock;
            statusEffects.Growth += 3;


            card.Energy.CurrentEnergy -= cardInfoThis.EnergyCost;

            deck.Exhaust(gameObject);


            //Fix the deck
            FixTheDeck();
        }




        //value of wealth
        if (cardInfoThis.Name == "Value of Wealth" || cardInfoThis.Name == "Value of Wealth")
        {
            card.RemoveCardFromHand();

            //Remove energy and deal damage
            card.EnemyHealth.CurrentHealth -= (statusEffects.totalDeckRarity + cardInfoThis.DamageOrBlock);
            card.Energy.CurrentEnergy -= cardInfoThis.EnergyCost;


            DamageText(statusEffects.totalDeckRarity.ToString());
            //Fix the deck
            FixTheDeck();
        }


        // 2 Steps Ahead
        if (cardInfoThis.Name == "2 Steps Ahead" || cardInfoThis.Name == "2 Steps Ahead+")
        {
            card.RemoveCardFromHand();

            //Remove energy and deal damage

            deck.DrawCard();
            deck.DrawCard();

            if(cardInfoThis.Name == "2 Steps Ahead+")
            {
                card.Energy.CurrentEnergy += 2;
                print("Upgraded 2 steps ahead");
            }
            card.Energy.CurrentEnergy -= cardInfoThis.EnergyCost;

            //Fix the deck
            FixTheDeck();
        }





        card.Energy.UpdateEnergy();
    }


    private void FixTheDeck()
    {
        card.currentDeck.UpdatePositions();
        Discard.Discard(gameObject);
    }
    private void ApplyAdditionalText()
    {
        if (card!= null && cardInfoThis.Name == "Value of Wealth")
        {
            string text = "( "+ statusEffects.totalDeckRarity + " Damage )";
            description.Description.text = description.Description.text + "\n" + text;
        }
     }

    private void DamageText(string ChangeToThis)
    {
        DamageTextInstance.GetComponent<TextMesh>().text = ChangeToThis;
    }

}
