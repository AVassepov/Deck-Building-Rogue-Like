using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RestButtons : MonoBehaviour
{


    [Header("0 = heal, 1 = Upgrade , 2 = Cher Ami")]
    public int ActionID;
    private Rest rest;

    [SerializeField] private GameObject UpgradeUI;
    private void Start()
    {
        rest = transform.parent.gameObject.GetComponent<Rest>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void Heal(PlayerHealth Health)
    {
        Health.Heal(Health.CurrentHP / 4);

        if(rest.CurrentPlayer == rest.Player1)
        {
            rest.CurrentPlayer = rest.Player2;
        }
        else
        {
            FindObjectOfType<DungeonNavigation>().CanMove = true;
            FindObjectOfType<EncounterManager>().CurrentRoom.ClearRoom();
            Destroy(transform.parent.gameObject);
        }
    }

    private void UpgradeCard(Deck Deck)
    {

    GameObject ShowDeckForUpgrade=     Instantiate(UpgradeUI, new Vector3(0, 3, -1f), Quaternion.identity);

        rest.UpgradeUIInstance = ShowDeckForUpgrade;

        DisplayDeck display = ShowDeckForUpgrade.GetComponentInChildren<DisplayDeck>();
        display.deck1 = rest.CurrentPlayer.GetComponent<Deck>();
        display.isUpgrading = true;
    }




    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(ActionID == 0)
            {
                Heal(rest.CurrentPlayer.GetComponent<PlayerHealth>());
            }
            else if(ActionID == 1) 
            {
                UpgradeCard(rest.CurrentPlayer.GetComponent<Deck>());
            }
           
        }
   }




}
