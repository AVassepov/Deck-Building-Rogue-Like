using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D20 : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] Transform Target;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Target = GameObject.Find("Dice Target").transform;

        rb.AddForce(Target.position - transform.position , ForceMode.Impulse);
        rb.angularVelocity = new Vector3(Random.Range(10f, 30f), Random.Range(10f, 30f), Random.Range(10f, 30f));
    }

}
