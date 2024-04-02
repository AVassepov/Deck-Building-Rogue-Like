using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowD20 : MonoBehaviour
{
    [SerializeField] private GameObject Dice;
    private Rigidbody rb;

     //rb = Dice.GetComponent<Rigidbody>();
 

    void Start()
    {
        
    }


    void Update()
    {
        GameObject temp = null;

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Dice Thrown");

            temp = Instantiate(Dice, transform.position, Quaternion.identity );

            temp.transform.parent = null;
        }
    }
}
