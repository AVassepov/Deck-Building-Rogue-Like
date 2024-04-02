using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayer : MonoBehaviour
{
    public PlayerHealth CurrentPlayerHealth;

    public bool CardIsSelected;

    public GameObject SelectedCard;

    public List<GameObject> AllPlayers;

    // Start is called before the first frame update
    void Awake()
    {
        AllPlayers.AddRange(GameObject.FindGameObjectsWithTag("Player"));
    }
}
