using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCombatGiveMoney : MonoBehaviour
{
    private SpawnCoins bank;

    public GameObject OtherButton;


    public CombatManager CombatManager;

    private RewardManager RewardManager;


    private void Awake()
    {
       bank = FindObjectOfType<SpawnCoins>();

            transform.Find("Text").GetComponent<MeshRenderer>().sortingLayerName = "Show Pile Text";

        RewardManager = FindObjectOfType<RewardManager>();


    }

    private void Start()
    {

        if (CombatManager.AlreadyPlundered)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;


        }
    }


    private void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0) && !CombatManager.AlreadyPlundered)
        {
            int NewMoney = Random.Range(10, 25);


            CombatManager.Continue();

            bank.SavedMoney += NewMoney;
            bank.TotalMoney = bank.SavedMoney;

            GetArticaft();


            CombatManager.AlreadyPlundered = true; 

            bank.ClearCoins();
            bank.CurrentMoney.Clear();
            bank.ResetMoney();

            CombatManager.FinishCombat();

            Destroy(OtherButton);
            Destroy(gameObject);

        }
    }

    void GetArticaft()
    {

        float chance = Random.Range(0, 100);

        print(chance);
        if (chance<RewardManager.ArtifactChance)
        {
            RewardManager.ArtifactChance = 10;
            GiveArtifact();
        }
        else
        {
            RewardManager.ArtifactChance += 2.5f;
        }
    }

    void GiveArtifact()
    {

        float chance = Random.Range(0, 100);



        if(chance< RewardManager.LegendaryChance)
        {
            print("Got A legendary");
        }
        else if (chance < RewardManager.LegendaryChance + RewardManager.RareChance)
        {
            print("Got A Rare");
        }
        else
        {
            print("Got A Common");
        }
    }

}
