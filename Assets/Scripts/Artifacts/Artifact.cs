using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Artifact : MonoBehaviour
{

    [SerializeField] private TextMesh Name;

    private TurnManager TurnManager;

    [SerializeField] private GameObject DetailsWindow;

    [SerializeField] private GameObject PopUpWindow;
    private GameObject PopUpWindowIntance;

    private ArtifactInfo info;

    public StatusEffects playerStatuses;

    public PlayerHealth health;

    [SerializeField] private ArtifactInfo Info;

    public Deck PlayerDeck;

    // Start is called before the first frame update
    void Start()
    {
        Name = GetComponentInChildren<TextMesh>();
        ApplyEffect();
        info = gameObject.GetComponent<ArtifactDataReader>().ArtifactInfo;


        SetStartingRelics();

        TurnManager = GameObject.Find("Managers").GetComponent<TurnManager>();
    }


    private void OnMouseOver()
    {

        if (PopUpWindowIntance == null)
        {
            PopUpWindowIntance = Instantiate(PopUpWindow);
            PopUpWindowIntance.transform.position = new Vector3(transform.position.x + 3.15f, transform.position.y - 2.5f, transform.position.z);

            //Change Text

            PopUpWindowIntance.transform.GetChild(0).GetComponent<TextMesh>().text = info.Name;
            PopUpWindowIntance.transform.GetChild(1).GetComponent<TextMesh>().text = info.Description;
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject moreInfoInstance = Instantiate(DetailsWindow, new Vector3(0, 3.23000002f, -1.17999995f), Quaternion.identity);
            moreInfoInstance.GetComponent<MoreArtifactInfo>().info = Info;
        }
    }

    void OnMouseExit()
    {
        if (PopUpWindowIntance != null)
        {
            Destroy(PopUpWindowIntance);
        }
    }

    private void SetStartingRelics()
    {
        if (info.Name == "Blood Curse")
        {
            health = GameObject.Find("Player2").GetComponent<PlayerHealth>();
            playerStatuses = GameObject.Find("Player2").GetComponent<StatusEffects>();
            GameObject.Find("Managers").GetComponent<ArtifactManager>().ArtifactsListP2.Add(this);
        }
        else if (info.Name == "Deck Of Cards")
        {
            health = GameObject.Find("Player1").GetComponent<PlayerHealth>();
            playerStatuses = GameObject.Find("Player1").GetComponent<StatusEffects>();
            GameObject.Find("Managers").GetComponent<ArtifactManager>().ArtifactsListP1.Add(this);
            PlayerDeck = GameObject.Find("Player1").GetComponent<Deck>();
        }

    }


    void ApplyEffect()
    {
        //example for other Artifacts
        /* if (Info.Name == "Test")
         {
             playerStatuses.Strength += 3;

         }*/
    }

    public void Apply3HitComboEffect()
    {
        if (Info.Name == "Test")
        {
            playerStatuses.Strength += 1;

            print("Added 1 strength");

        }
    }
    public void OnEnemyDeath()
    {
        print("Enemy died");


        if (Info.Name == "Blood Curse")
        {
            health.Heal(4);

            print("Healed 4 hp");

        }

    }

    public void TurnStartArtifactEffect()
    {

        if (Info.Name == "Deck Of Cards" && TurnManager.CurrentDeck == TurnManager.deck)
        {
            PlayerDeck.DrawCard();
            TurnManager.SelectAndDiscard(1);

        }
    }

    public void BloodCurseSelfDamage(bool DidKill)
    {
        if (!DidKill)
        {
            health.TakeDamage(8);
        }

    }
}
