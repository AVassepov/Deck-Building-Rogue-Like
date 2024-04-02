using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{


    public float ArtifactChance = 10;

    // lists of artifacts 
    public GameObject[] CommonArtifacts;

    public GameObject[] RareArtifacts;

    public GameObject[] LegendaryArtifacts;

    public GameObject[] BossArtifacts;

    public GameObject[] ShopArtifacts;


    // rarity chances
    public float LegendaryChance = 10;
    public float RareChance = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
