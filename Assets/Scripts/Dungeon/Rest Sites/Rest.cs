using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Rest : MonoBehaviour
{
    public GameObject Player1, Player2 ,CurrentPlayer;
    public List<GameObject> UpgradedCardsList;
    // Start is called before the first frame update

    public GameObject UpgradeUIInstance;

    public GameObject UpgradeThis;
    [SerializeField] private GameObject UIArrow;
    [SerializeField] private GameObject ConfirmUpgradeButton;

    // Dialgue stuff 

    [Header("Dialogue")]
    [SerializeField] private List<RestDialogue> restDialogueListFloor1;
    [SerializeField] private List<RestDialogue> restDialogueListFloor2;
    [SerializeField] private List<RestDialogue> restDialogueListFloor3;
    [SerializeField] private GameObject DialogueBubble;
    [SerializeField] private RestDialogue CurrentRestDialogue;
    private int CurrentDialogueCycle;
    private GameObject LastDialogueInstance;


    void Start()
    {
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");

        CurrentPlayer = Player1;

        ShowDialogue();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpgradeCard(ApplyDataToCard CardUsed)
    {
        GameObject Saved = FindUpgrade(CardUsed.CardInfo);

        Deck savedDeck = CurrentPlayer.GetComponent<Deck>();
        for (int i = 0; i < savedDeck.TotalCards.Count; i++)
        {

            print(savedDeck.TotalCards[i].GetComponent<ApplyDataToCard>().CardInfo.Name + " checked against " + CardUsed.CardInfo.Name);



            if (savedDeck.TotalCards[i].GetComponent<ApplyDataToCard>().CardInfo.Name == CardUsed.CardInfo.Name)
            {

                UpgradeUIInstance.transform.GetChild(1).transform.position = new Vector3(1000,1000,1000);


                GameObject Arrow =Instantiate(UIArrow);

                Arrow.transform.parent = UpgradeUIInstance.transform;

                //Unupgraded card
                GameObject Card1 =  Instantiate(savedDeck.TotalCards[i], new Vector3(-3, 3, -3) , quaternion.identity) ;
                Card1.GetComponent<ApplyDataToCard>().ResetLayerForUpgrades();
                Card1.GetComponent<ApplyDataToCard>().isUpgrading = true;
                Card1.transform.parent = UpgradeUIInstance.transform;

                //Upgraded card
                GameObject Card2 = Instantiate(Saved, new Vector3(3, 3, -3), quaternion.identity);
                Card2.GetComponent<ApplyDataToCard>().ResetLayerForUpgrades();
                Card2.GetComponent<ApplyDataToCard>().isUpgrading = true;
                Card2.transform.parent = UpgradeUIInstance.transform;

                GameObject Temp =  Instantiate(ConfirmUpgradeButton , new Vector3(0, 0.76f, -2.14f), Quaternion.identity);
                Temp.transform.parent = UpgradeUIInstance.transform;


                UpgradeCards upgrade = Temp.GetComponent<UpgradeCards>();
                upgrade.i = i;
                upgrade.savedDeck = savedDeck;
                upgrade.CardUsed = CardUsed;
                upgrade.rest = this;
                upgrade.Saved = Saved;
                upgrade.Card1 = Card1;
                upgrade.Card2 = Card2; 
                upgrade.Arrow = Arrow;

            }


      
        }


    }


    public GameObject FindUpgrade(CardInfo CardUsed)
    {
        string UpgradedCardName = CardUsed.Name + "+";
        print(UpgradedCardName);


        for (int i = 0; i < UpgradedCardsList.Count; i++)
        {
            if (UpgradedCardsList[i].GetComponent<ApplyDataToCard>().CardInfo.Name == UpgradedCardName)
            {
                print("Successfully found upgraded card");
                return UpgradedCardsList[i];
            }
        }
        return null;

    }


     private void ShowDialogue()
    {

        if(CurrentRestDialogue.ListOfDialogues.Count> CurrentDialogueCycle)
        {
            print(CurrentRestDialogue.ListOfDialogues[CurrentDialogueCycle]);
            CurrentDialogueCycle++;
            StartCoroutine(CycleDialogue());

        }
    }

    IEnumerator CycleDialogue()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(4);
        GameObject DialogueInstance = Instantiate(DialogueBubble);
    

        ShowDialogue();
    }

}
