using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

public class SpawnCoins : MonoBehaviour
{

    public GameObject[] CoinTypes;
    private int[] CoinValues = { 100, 50, 20, 10, 5, 1 };

    public List<GameObject> CurrentMoney;


    public int TotalMoney;

    public int SavedMoney;

    public List<GameObject> SavedCoins;

    private void Start()
    {
        ResetMoney();


        }


    public void CycleThroughValues(int currentValue)
    {

        while (TotalMoney >= CoinValues[currentValue])
        {
            CurrentMoney.Add(CoinTypes[currentValue]);
            TotalMoney -= CoinValues[currentValue];
        }

    }

    public void ResetMoney(){
        SavedMoney = TotalMoney;
        for (int i = 0; i < CoinTypes.Length; i++)
        {
            CycleThroughValues(i);
        }

        SpawnInCoins();



    }



    public void SpawnInCoins()
    {
        for (int i = 0; i < CurrentMoney.Count; i++)
        {
            StartCoroutine(SpawnNext((float)i, i));
        }

    }

    IEnumerator SpawnNext( float delay, int current)
    {
        
        yield return new WaitForSeconds(delay/ 5);

        Vector3 SpawnPoint;
        SpawnPoint = new Vector3(transform.position.x+Random.Range(-3,3), transform.position.y + Random.Range(-3, 3), transform.position.z + Random.Range(-3, 3));
        GameObject temp;
        temp = Instantiate(CurrentMoney[current], SpawnPoint, Random.rotation);
       // print(temp.name);

    }


    public void ClearCoins()
    {
        for (int i = 0; i < SavedCoins.Count; i++)
        {
            Destroy(SavedCoins[i]);
        }
        SavedCoins.Clear();
    }

}
