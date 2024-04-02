using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ReturnD20Number : MonoBehaviour
{
    [SerializeField] private D20 D20;
    [SerializeField] private Rigidbody D20Rb;
    [SerializeField] private int Number;
    private EnemyHealth Enemy;

    private bool landed;
    private Vector3 normalPositionOffset;

    private void Awake()
    {
        SelectEnemy selectEnemy = GameObject.Find("Managers").GetComponent<SelectEnemy>();
        normalPositionOffset = transform.localPosition;
        Enemy = selectEnemy.AllEnemies[Random.Range(0, selectEnemy.AllEnemies.Count)].GetComponent<EnemyHealth>(); 
    }

    private void Update()
    {
        if (D20Rb.velocity == Vector3.zero)
        {

            // landed=true;

            StartCoroutine(Landing());

        }
        else
        {
            StopAllCoroutines();

        }
        Vector3 rotatedOffset = D20Rb.rotation * normalPositionOffset; //Rotates the offset vector by the parent's rotation

        transform.position = D20Rb.position + rotatedOffset;
    }



    private void OnTriggerStay(Collider other)
    {

        if (landed && other.tag != "Dice")
        {

            Debug.Log(Number);
            Destroy(transform.parent.gameObject);
            Enemy.CurrentHealth = Enemy.CurrentHealth - Number;
        }
    }

    IEnumerator Landing()
    {
        yield return new WaitForSeconds(1.5f);

        landed = true;

    }

}
