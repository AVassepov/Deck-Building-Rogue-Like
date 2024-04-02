using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatManager : MonoBehaviour
{
     private SelectEnemy SelectEnemy;

    private Deck Player1Deck;
    private Deck Player2Deck;
    private Deck CurrentDeck;

    public int RareChance = 35;

    public int LegendarChance = 15;

    [SerializeField] private PossibleCards Player1Rewards;
    [SerializeField] private PossibleCards Player2Rewards;

    private PossibleCards currentRewardsList;


    private GameObject[] Rewards = new GameObject[3];

    private float ArtifactChance = 10;


    private int PlayersGotTheirRewards;
    public bool AlreadyPlundered;

    [Header("Graphic elements")]
    [SerializeField] private GameObject BackGround;
    [SerializeField] private GameObject CardsButton;
    [SerializeField] private GameObject MoneyButton;
    private TurnManager turnManager;

    private GameObject backGroundInstance;

    // Start is called before the first frame update
    void Start()
    {

        turnManager = gameObject.GetComponent<TurnManager>();
        SelectEnemy = gameObject.GetComponent<SelectEnemy>();
        Player1Deck = turnManager.deck;
        Player2Deck = turnManager.deck2;

        CurrentDeck = Player1Deck;
        currentRewardsList = Player1Rewards;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CheckGameEnd()
    {
        if (SelectEnemy.AllEnemies.Count.Equals(1))
        {
            print("COMBAT ENDED");
            EndCombat();
        }


    }


    public void EndCombat()
    {
        for (int i = 0; i < 3; i++)
        {

            GameObject Reward = ChooseCard();
            Rewards[i] = Reward;

            turnManager.ClearClearableEffects();

        }

        gameObject.GetComponent<ArtifactManager>().BloodCurseKillCheck();
        gameObject.GetComponent<ArtifactManager>().VagabondKilledThisTurn = false;

       backGroundInstance =   Instantiate(BackGround, new Vector3(0.03f, 3.26f, -0.85f), Quaternion.identity);

        SpawnButtons();
        TurnManager turn = gameObject.GetComponent<TurnManager>();

        turn.energy.CurrentEnergy = turn.energy.MaximumEnergy;
    }

    private GameObject ChooseCard()
    {
        int roll =  Random.Range(1, 100);
        GameObject selectedCard;


        print ("Rolled a : "+ roll);
        if (roll <= LegendarChance)
        {
            selectedCard = currentRewardsList.LegendaryCards[Random.Range(0, currentRewardsList.LegendaryCards.Count)];
        }else if (roll <= LegendarChance+ RareChance)
        {

            selectedCard = currentRewardsList.RareCards[Random.Range(0, currentRewardsList.RareCards.Count )];
        }
        else
        {

            selectedCard = currentRewardsList.UncommonCards[Random.Range(0, currentRewardsList.UncommonCards.Count)];

        }

        return selectedCard;

    }



    public void AddToDeck(GameObject ChosenCard)
    {
        CurrentDeck.TotalCards.Add(ChosenCard);

    }
    

    public void Continue()
    {
        if(CurrentDeck == Player1Deck)
        {
            CurrentDeck = Player2Deck;
            currentRewardsList = Player2Rewards;

            for (int i = 0; i < 3; i++)
            {

                GameObject Reward = ChooseCard();
                Rewards[i] = Reward;
            }


            SpawnButtons();


        }
        else
        {
            CurrentDeck = Player1Deck;
            currentRewardsList = Player1Rewards;
            Destroy(backGroundInstance);
            AlreadyPlundered = false;
        }

    }




    private void SpawnButtons()
    {


        GameObject moneyButton, cardButton;
        moneyButton = Instantiate(MoneyButton, new Vector3(-2, 3, -2), Quaternion.identity);
        moneyButton.GetComponent<EndCombatGiveMoney>().CombatManager = this;


        cardButton = Instantiate(CardsButton, new Vector3(2, 3, -2), Quaternion.identity);
        cardButton.GetComponent<EndCombatGetCard>().Rewards = Rewards;
        cardButton.GetComponent<EndCombatGetCard>().Rewards = Rewards;
        cardButton.GetComponent<EndCombatGetCard>().Deck = CurrentDeck;
        cardButton.GetComponent<EndCombatGetCard>().CombatManager = this;

        moneyButton.GetComponent<EndCombatGiveMoney>().OtherButton = cardButton;
        cardButton.GetComponent<EndCombatGetCard>().OtherButton = moneyButton;

    }

    public void FinishCombat()
    {
        PlayersGotTheirRewards++;
        if (PlayersGotTheirRewards== 2)
        {
            FindObjectOfType<DungeonNavigation>().CanMove = true;
        }

        FindObjectOfType<DungeonNavigation>().currentRoom.ClearRoom();

        turnManager.CurrentDeck.DiscardHand();
        turnManager.deck.ShuffleDeck();
        turnManager.deck2.ShuffleDeck();


        turnManager.InCombat = false;
        turnManager.CurrentDeck = turnManager.deck;
        turnManager.Cycle = 0;

        print("Finished Combat");

        if(CurrentDeck == Player2Deck)
        {
            turnManager.ChangePlayer();

        }
    }
}
