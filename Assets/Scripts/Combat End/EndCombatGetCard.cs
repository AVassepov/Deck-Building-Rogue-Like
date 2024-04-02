using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCombatGetCard : MonoBehaviour
{
    public GameObject[] Rewards = new GameObject[3];

    public GameObject OtherButton;

    public CombatManager CombatManager;

    public Deck Deck;

    private void Awake()
    {
        transform.Find("Text").GetComponent<MeshRenderer>().sortingLayerName = "Show Pile Text";
    }



    private void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0))
        {
            GameObject[] Cards = new GameObject[3];
            for (int i = 0; i < Rewards.Length; i++)
            {
                GameObject tempcard;

                tempcard= Instantiate(Rewards[i] , new Vector3(-3 + i * 3,3,-2), Quaternion.identity);
                Destroy(tempcard.GetComponent<Card>());
                tempcard.AddComponent<TopSortingLayer>();
                AddThisToDeck addToDeck;

                addToDeck= tempcard.AddComponent<AddThisToDeck>();
                addToDeck.Deck = Deck;
                addToDeck.Manager = CombatManager;
                addToDeck.ThisCard = Rewards[i];
                Cards[i] = tempcard;
            }

            Cards[0].GetComponent<AddThisToDeck>().OtherCard1 = Cards[1];
            Cards[0].GetComponent<AddThisToDeck>().OtherCard2 = Cards[2];

            Cards[1].GetComponent<AddThisToDeck>().OtherCard1 = Cards[0];
            Cards[1].GetComponent<AddThisToDeck>().OtherCard2 = Cards[2];

            Cards[2].GetComponent<AddThisToDeck>().OtherCard1 = Cards[1];
            Cards[2].GetComponent<AddThisToDeck>().OtherCard2 = Cards[0];

            //CombatManager.Continue();

            CombatManager.FinishCombat();

            Destroy(OtherButton);
            Destroy(gameObject);


        }
    }
}
