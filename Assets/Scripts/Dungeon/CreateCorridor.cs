using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCorridor : MonoBehaviour
{
    private void Awake()
    {
        Invoke("SelfDestruct", 0.5f);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.GetComponent<CreateCorridor>()==null)
        {
            Destroy(collision.gameObject);
         //   Destroy(gameObject);
        }
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
