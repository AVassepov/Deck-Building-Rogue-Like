using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Card : MonoBehaviour
{
    [Header("Managers And Components")]
    public CardInfo CardInfo;
    public TurnManager turnManager;
    public EnemyHealth EnemyHealth;
    public PlayerHealth CurrentPlayerHealth;
    public StatusEffects CurrentPlayerStatusEffects;
    public CardEffect cardEffect;
    public StatusEffects statusEffects;

    [HideInInspector]
    public CardDiscardManager DiscardManager;


    [Header("Graphics")]
    public GameObject DamageText;



    private ArtifactManager ArtifactManager;

    [HideInInspector]
    public Energy Energy;
    [HideInInspector]
    public Deck currentDeck;

    public int PositionInHand;


    
    [HideInInspector]
    public SelectEnemy selectEnemy;

    [HideInInspector]
    public SelectPlayer selectPlayer;

    [Header("Bools")]

    public bool isDiscarding;

    public bool ChosenToDiscard;

    [HideInInspector]
    public Vector3 DiscarderPosition;
    private LineRenderer line;

    private bool selected;

    private DiscardCard Discard;

    // Start is called before the first frame update
    void Awake()
    {
        Energy = GameObject.Find("Player Energy").GetComponent<Energy>();
        turnManager = GameObject.Find("Managers").GetComponent<TurnManager>();
        turnManager.UpdateCurrentPlayer(this);

        selectEnemy = GameObject.Find("Managers").GetComponent<SelectEnemy>();
        selectPlayer = GameObject.Find("Managers").GetComponent<SelectPlayer>();

        line = gameObject.GetComponent<LineRenderer>();

        cardEffect = gameObject.GetComponent<CardEffect>();

        Discard = GameObject.Find("Discard Cards").GetComponent<DiscardCard>();

        ArtifactManager = statusEffects.gameObject.GetComponent<ArtifactManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if(selectEnemy.CurrentEnemy!= null) {
            EnemyHealth = selectEnemy.CurrentEnemy;
        }
        else if (selectPlayer.CurrentPlayerHealth != null)
        {
            CurrentPlayerHealth = selectPlayer.CurrentPlayerHealth;
        }


        if (ChosenToDiscard)
        {
            transform.position = Vector3.Lerp(transform.position, DiscarderPosition, 0.05f);
        }

        if (selected)
        {
            if (Input.GetMouseButton(0) && Energy.CurrentEnergy >= CardInfo.EnergyCost && !isDiscarding && !ChosenToDiscard)
            {

                DrawLine();
            }
            else if (isDiscarding && Input.GetMouseButtonUp(0) && DiscardManager.CardsToDiscard> DiscardManager.CurrentlyDiscarding)
            {
                DiscardManager.DiscardList.Add(this);
                DiscardManager.CurrentlyDiscarding++;
                ChosenToDiscard = true;
                isDiscarding = false;
                selected = false;

                DiscarderPosition = new Vector3(transform.position.x, transform.position.y + 5.2f, transform.position.z);
            }
            else if (ChosenToDiscard && Input.GetMouseButtonUp(0))
            {
                DiscardManager.DiscardList.Remove(this);
                DiscardManager.CurrentlyDiscarding--;
                ChosenToDiscard = false;
                isDiscarding = true;
                selected = false;
                DiscarderPosition = DiscarderPosition - new Vector3(0, -5.2f, 0);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                line.enabled = false;
                selected = false;

                if (EnemyHealth != null && selectEnemy.CurrentEnemy!= null && Energy.CurrentEnergy >= CardInfo.EnergyCost && statusEffects.CanAttackStacks < 1 && CardInfo.Type == "Attack")
                {
                    GameObject DamageTextInstance = Instantiate(DamageText, selectEnemy.CurrentEnemy.transform);
                    DamageTextInstance.transform.parent = null;

                    cardEffect.DamageTextInstance = DamageTextInstance;
                    cardEffect.PlayCard();
                    selectEnemy.CurrentEnemy.UpdateHealthValue();


                    ArtifactManager.AttackCounter++;

                    Destroy(this);
                }
                else if (EnemyHealth != null && selectEnemy.CurrentEnemy != null && Energy.CurrentEnergy >= CardInfo.EnergyCost && CardInfo.Type == "Targeted Action")
                {
                    cardEffect.PlayCard();

                    Destroy(this);
                }
                else if (currentDeck.NotAimingAtHand && Energy.CurrentEnergy >= CardInfo.EnergyCost && CardInfo.Type == "Action" && statusEffects.PanicStacks < 1)
                {
                    cardEffect.PlayCard();

                    Destroy(this);
                } else if (CurrentPlayerHealth != null && selectPlayer.CurrentPlayerHealth != null && Energy.CurrentEnergy >= CardInfo.EnergyCost && CardInfo.Type == "Player Action" && statusEffects.PanicStacks < 1)
                {
                    cardEffect.PlayCard();

                    Destroy(this);
                }



                // selection of a player or enemy for a targeted card
                selectEnemy.CardIsSelected = false;
                selectEnemy.SelectedCard = null;

                selectPlayer.CardIsSelected = false;
                selectPlayer.SelectedCard = null;
            }
        }
        else
        {
            Clear();
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!selectEnemy.CardIsSelected && selectEnemy.SelectedCard == null)
            {
                selected = true;
                selectEnemy.CardIsSelected = true;
                selectEnemy.SelectedCard = gameObject;
            }

            if (!selectPlayer.CardIsSelected && selectPlayer.SelectedCard == null)
            {
                selected = true;
                selectPlayer.CardIsSelected = true;
                selectPlayer.SelectedCard = gameObject;
            }

        }

    }

    private void OnMouseExit()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Clear();
        }
    }
    private void DrawLine()
    {
            line.enabled = true;

            line.positionCount = 2;

        Vector3 worldPosition;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        line.SetPosition(0,transform.position);
        line.SetPosition(1, worldPosition);
    }



    public void RemoveCardFromHand()
    {
        print(CardInfo.Name);
        //Remove Card From Hand
        currentDeck.Discarded.Add(currentDeck.Hand[PositionInHand]);
        currentDeck.Hand.RemoveAt(PositionInHand);
        currentDeck.currentHand.RemoveAt(PositionInHand);

    }



    private void Clear()
    {

        //select target enemy
        selectEnemy.CardIsSelected = false;
        selectEnemy.SelectedCard = null;
        EnemyHealth = null;

        //select target player
        selectPlayer.CardIsSelected = false;
        selectPlayer.SelectedCard = null;
        CurrentPlayerHealth = null;

    }
}
