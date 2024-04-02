using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Energy energy;



    [Header("Decks")]
   public Deck deck;
    public Deck deck2;
    public Deck CurrentDeck;


    [Header("Health")]
    public PlayerHealth health;
    public  PlayerHealth health2;
    public PlayerHealth CurrentHealth;

    [Header("Status Effects")]
    [SerializeField] private StatusEffects statusEffects;
    [SerializeField] private StatusEffects statusEffects2;
    public StatusEffects CurrentStatusEffects;


    [Header("Cycle")]
    public int Cycle;
    public bool CanContinue;
    public bool InCombat;

    [Header("Other Managers")]
    [SerializeField] private ArtifactManager artifactManager;


    [Header("Graphics")]
    [SerializeField] private GameObject DiscardBackGround;
    [SerializeField] private GameObject DiscardBackGroundInstance;

    private bool PlayerTurn = true;

    //  [Header("Events")]

    private void Awake()
    {
        CurrentDeck = deck;
        CurrentStatusEffects = statusEffects;
        CurrentHealth = health;
        artifactManager = gameObject.GetComponent<ArtifactManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
       // CurrentDeck.DrawCards();
        if (gameObject.GetComponent<SelectEnemy>().AllEnemies.Count>0) {
            InCombat = true;
            artifactManager.CheckAllTurnStartArtifacts(artifactManager.ArtifactsListP1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanContinue && InCombat)
        {
                EndTurn();
                PlayerTurn = false;
           
        }

    }


    public void StartTurn()
    {
        artifactManager.AttackCounter = 0;

        artifactManager.CheckAllTurnStartArtifacts(artifactManager.ArtifactsListP1);
        //CurrentDeck.DrawCard();

        /* if (!artifactManager.isPlayer2)
         {
             artifactManager.CheckAllTurnStartArtifacts(artifactManager.ArtifactsListP1);
         }
         else
         {
             artifactManager.CheckAllTurnStartArtifacts(artifactManager.ArtifactsListP2);
         }*/



        if (CurrentHealth.CurrentHP > 0)
        {
            if (CurrentDeck.DeckOfCards.Count >= 5)
            {

                CurrentDeck.DrawCards();
            }
            else
            {
                CurrentDeck.ShuffleDeck();
                CurrentDeck.DrawCards();


            }
            DoStatuses();
            ApplyInsanity();

        }  else if ( CurrentHealth.CurrentHP < 0 && !(health.CurrentHP < 0 && health2.CurrentHP < 0)) 
        {
            EndTurn();
        }

    }

    private void EndTurn()
    {

        energy.CurrentEnergy = energy.MaximumEnergy;
        CurrentDeck.DiscardHand();
        PlayerTurn = false;
        if(CurrentDeck == deck) {
        ChangePlayer();
        StartTurn();

        }
        else
        {
            ChangePlayer();
            PlayEnemyTurn();
            StartTurn();

            health.RemoveBlock();
            health2.RemoveBlock();
        }
    }


    private void PlayEnemyTurn()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyActions>().PlayAction();

        }


    }


    public void ApplyInsanity()
    {
        for (int i = 0; i < CurrentStatusEffects.Instanity; i++)
        {
            int random = Random.Range(0, 2);
            print("Instanity Roll: " +  random);
            if (random == 0)
            {
                CurrentDeck.DiscardRandom();
            }
            else
            {
                CurrentDeck.DrawCard();
            }

        }

        CurrentDeck .UpdatePositions();
    }



    public void ChangePlayer()
    {
        if(CurrentDeck == deck)
        {
            CurrentDeck = deck2;
            CurrentHealth = health2;
            CurrentStatusEffects = statusEffects2;
            artifactManager.isPlayer2 = true;
        }
        else if (CurrentDeck == deck2)
        {
            CurrentDeck = deck;
            CurrentHealth = health;
            CurrentStatusEffects = statusEffects;
            artifactManager.isPlayer2 = false;

            Cycle++;
        }

    }


    public void UpdateCurrentPlayer(Card card )
    {
        card.currentDeck = CurrentDeck;
        card.statusEffects = CurrentStatusEffects;

    }


    public void DoStatuses()
    {
        if (CurrentStatusEffects.CanAttackStacks > 0)
        {
            CurrentStatusEffects.CanAttackStacks--;

        }
        if (CurrentStatusEffects.FearStacks > 0)
        {
            CurrentStatusEffects.FearStacks--;

        }
        if (CurrentStatusEffects.Growth > 0)
        {
            CurrentStatusEffects.Strength = CurrentStatusEffects.Strength + CurrentStatusEffects.Growth;

        }
        if (CurrentStatusEffects.PanicStacks > 0)
        {
            CurrentStatusEffects.PanicStacks--;

        }
    }

    public void SelectAndDiscard(int AmountToDiscard)
    {
        CanContinue = false;
        DiscardBackGroundInstance = Instantiate(DiscardBackGround, new Vector3(0.11f, 3.3f, -0.09f) , Quaternion.identity);

        DiscardBackGroundInstance.transform.GetChild(0).GetComponent<CardDiscardManager>().DeckUsed = CurrentDeck;
        DiscardBackGroundInstance.transform.GetChild(0).GetComponent<CardDiscardManager>().CardsToDiscard = AmountToDiscard;

        for (int i = 0; i < CurrentDeck.currentHand.Count; i++)
        {
            CurrentDeck.currentHand[i].GetComponent<Card>().isDiscarding = true;
            CurrentDeck.currentHand[i].GetComponent<Card>().DiscardManager = DiscardBackGroundInstance.transform.GetChild(0).GetComponent<CardDiscardManager>();
        }


    }


    public void ClearClearableEffects()
    {
        statusEffects.Finnese = 0;
        statusEffects.FearStacks = 0;
        statusEffects.PanicStacks = 0;
        statusEffects.Strength = 0;
        statusEffects.Growth = 0;
        statusEffects.CanAttackStacks = 0;
        statusEffects.Riposting = false;
        statusEffects.RipostingDamage = 0;

        statusEffects2.Finnese = 0;
        statusEffects2.FearStacks = 0;
        statusEffects2.PanicStacks = 0;
        statusEffects2.Strength = 0;
        statusEffects2.Growth = 0;
        statusEffects2.CanAttackStacks = 0;
        statusEffects2.Riposting = false;
        statusEffects2.RipostingDamage = 0;
    }
}
