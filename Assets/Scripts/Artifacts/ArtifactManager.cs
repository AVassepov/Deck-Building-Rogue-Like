using System.Collections.Generic;
using UnityEngine;

public class ArtifactManager : MonoBehaviour
{

    public List<Artifact> ArtifactsListP1;
    public List<Artifact> ArtifactsListP2;
    public bool isPlayer2;

    public bool VagabondKilledThisTurn;
    public int AttackCounter;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        if (AttackCounter >= 3)
        {
            AttackCounter = 0;

            if(!isPlayer2)
            {
                CheckArtifactsFor3Attacks(ArtifactsListP1);
            }
            else
            {
                CheckArtifactsFor3Attacks(ArtifactsListP2);
            }

        }

    }



    void CheckArtifactsFor3Attacks(List<Artifact> currentlist)
    {

        for (int i = 0; i < currentlist.Count; i++)
        {
            if (currentlist[i] != null)
            {
                currentlist[i].Apply3HitComboEffect();
            }
        }

    }


    public void CheckOnEnemyDeath(List<Artifact> currentlist)
    {
        for (int i = 0; i < currentlist.Count; i++)
        {

            print(currentlist[i]);
            if (currentlist[i] != null)
            {
                currentlist[i].OnEnemyDeath();
            }
        }

    }

    public void CheckAllTurnStartArtifacts(List<Artifact> currentlist)
    {
        for (int i = 0; i < currentlist.Count; i++)
        {

            print(currentlist[i]);
            if (currentlist[i] != null)
            {
                currentlist[i].Invoke("TurnStartArtifactEffect" , 0.01f);
            }
        }
    }

    public void BloodCurseKillCheck()
    {
        ArtifactsListP2[0].BloodCurseSelfDamage(VagabondKilledThisTurn);
    }


    public void AddArtifact(List<GameObject> Artifacts, List<Artifact> Player , PlayerHealth health , StatusEffects status ,bool DoingPlayer2)
    {
        int Index = Random.Range(0, Artifacts.Count);

        Artifact artifact = Instantiate(Artifacts[Index]).GetComponent<Artifact>();
        artifact.health = health;
        artifact.playerStatuses = status;

        Player.Add(artifact);


        DisplayArtifacts artifactDisplay =  GameObject.Find("Artifacts Display").GetComponent<DisplayArtifacts>();

        if (!DoingPlayer2)
        {
            artifactDisplay.ArtifactsP1.Add(artifact.gameObject);
        }
        else
        {
            artifactDisplay.ArtifactsP2.Add(artifact.gameObject);
        }

        artifactDisplay.SetLocations(artifactDisplay.SavedList);

    }

}
