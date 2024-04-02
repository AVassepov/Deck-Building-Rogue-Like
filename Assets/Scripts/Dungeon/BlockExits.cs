using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockExits : MonoBehaviour
{
    public GameObject wall;
    private int SelfDestruct;
    private void Update()
    {
       /* if (SelfDestruct < 15)
        {
            SelfDestruct++;
        }
        else
        {
            Destroy(gameObject);
        }*/
    }


    public void Block() {

     /*   print("Blocked successfully");
        GameObject temp = Instantiate(wall, transform);
        temp.transform.SetParent(null);

        Destroy(gameObject);*/
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        print("Didnt make wall");
        Destroy(gameObject);
    }
}
