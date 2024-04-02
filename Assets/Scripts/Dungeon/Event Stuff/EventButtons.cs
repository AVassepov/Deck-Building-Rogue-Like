using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventButtons : MonoBehaviour
{

    //0 = Leave, 1 = fight, 2 GetItem, 3 GetResource, 4 WildCard
    public int outcome;

    private EncounterManager encounterManager;
    public Events SavedEvent;

    private bool DoForPlayer2;

    [SerializeField] private GameObject Player1,Player2;


    private void Awake()
    {
         encounterManager = FindObjectOfType<EncounterManager>();
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");
    }


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DoEvent();

            print("Doing player1");
        }
        if (Input.GetMouseButtonDown(1))
        {
            DoForPlayer2 = true;
            print("Doing player2");
            DoEvent();
        }
    }

    private void DoEvent()
    {
        if(outcome == 1)
        {

            List<CombatEncounter> combatevents = new List<CombatEncounter>();
            combatevents.Add(SavedEvent.SpecifiedFight);
            combatevents.Add(SavedEvent.SpecifiedFight);


            encounterManager.StartCombat(combatevents);
        }else if (outcome == 2)
        {
            List<GameObject> Artifacts = new List<GameObject>();
            Artifacts = SavedEvent.PossibleArtifacts;

            ArtifactManager artifactManager = FindObjectOfType<ArtifactManager>();

            if (!DoForPlayer2)
            {
                artifactManager.AddArtifact(Artifacts, artifactManager.ArtifactsListP1, Player1.GetComponent<PlayerHealth>(), Player1.GetComponent<StatusEffects>() , DoForPlayer2);
            }else
            {
                artifactManager.AddArtifact(Artifacts, artifactManager.ArtifactsListP2, Player2.GetComponent<PlayerHealth>(), Player2.GetComponent<StatusEffects>(), DoForPlayer2);
            }
            encounterManager.CurrentRoom.ClearRoom();
            FindObjectOfType<DungeonNavigation>().CanMove = true;
        }
        else
        {
            encounterManager.CurrentRoom.ClearRoom();

            FindObjectOfType<DungeonNavigation>().CanMove = true;
        }


        Destroy(transform.parent.gameObject);
    }


}
