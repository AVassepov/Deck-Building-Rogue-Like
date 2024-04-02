using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public int CurrentFloor = 0;
    [SerializeField] private List<CombatEncounter> Floor1Combat;
    [SerializeField] private List<Events> Floor1Events;

    public List<Events>[] ListOfEvents = new List<Events>[5];
    public List<CombatEncounter>[] ListOfCombat = new List<CombatEncounter>[5];


    [SerializeField] private GameObject EventUI;
    private ArtifactManager artifactManager;
    public RoomStage3 CurrentRoom;
    private CombatManager CombatManager;
    [SerializeField] private GameObject RestUI;


    // Start is called before the first frame update
    void Start()
    {
        CombatManager = gameObject.GetComponent<CombatManager>();
        ListOfCombat[0] = Floor1Combat;
        artifactManager = gameObject.GetComponent<ArtifactManager>();
        ListOfEvents[0] = Floor1Events;

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartCombat(List<CombatEncounter> encounters)
    {
        CombatManager.AlreadyPlundered = false;

        if (!CurrentRoom.WasCleared)
        {
            int randomEncounter = UnityEngine.Random.Range(0, encounters.Count);
            Vector3 SpawnLocation = new Vector3(0, 5.3f, -0.01f);


            for (int i = 0; i < encounters[randomEncounter].enemies.Count; i++)
            {
                SpawnLocation.x = (10 / encounters[randomEncounter].enemies.Count) * i;
                Instantiate(encounters[randomEncounter].enemies[i], SpawnLocation, Quaternion.identity);
            }


            TurnManager turnManager = gameObject.GetComponent<TurnManager>();
            turnManager.InCombat = true;
            turnManager.CurrentDeck = turnManager.deck;
            turnManager.StartTurn();
            turnManager.CurrentDeck.ShuffleDeck();
            //change this to combat start artifact check instead , when that's ready 
            //  artifactManager.CheckAllTurnStartArtifacts(artifactManager.ArtifactsListP1);
        }
    }


    public void StartEvent(List<Events> encounters)
    {
        if (!CurrentRoom.WasCleared)
        {
            int randomEncounter = UnityEngine.Random.Range(0, encounters.Count);

            print("Giving event UI an event");
           GameObject temp= Instantiate(EventUI, new Vector3(0, 3.2f, -0.87f), Quaternion.identity);

            ApplyEventData ApplyEvent = temp.GetComponent<ApplyEventData>();
            ApplyEvent.enabled = true;
            ApplyEvent.Event = encounters[randomEncounter];
        }
    }



    public void StartResting()
    {

        GameObject RestUIInstance = Instantiate(RestUI, new Vector3(0, 3.2f, -1f), Quaternion.identity);

        print("Resting");
    }


    //future
    public void Shop()
    {


    }


}
