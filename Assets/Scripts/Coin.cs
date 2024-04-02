using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{


    private void Awake()
    {
         FindObjectOfType<SpawnCoins>().SavedCoins.Add(gameObject) ;
    }




}
