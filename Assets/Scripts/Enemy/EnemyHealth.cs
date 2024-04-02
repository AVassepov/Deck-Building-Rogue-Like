using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private Slider Slider;
    public int CurrentHealth;
    [SerializeField] private int StartingHealth;
    private SelectEnemy manager;
    private CombatManager combatManager;



    public TextMesh HealthText;

    // Start is called before the first frame update
    void Start()
    {
        Slider.maxValue = StartingHealth;
        Slider.value = StartingHealth;
        CurrentHealth = StartingHealth;
        manager = GameObject.Find("Managers").GetComponent<SelectEnemy>();
        combatManager = GameObject.Find("Managers").GetComponent<CombatManager>();



        Slider.value = CurrentHealth;
        HealthText.text = CurrentHealth.ToString() + " / " + StartingHealth.ToString();

        FindObjectOfType<SelectEnemy>().AllEnemies.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
        if( CurrentHealth <= 0)
        {
            Die();

        }
    }


    private void Die()
    {
        print("died");

        //Check if any relics would proc from enemy death (this is only for the killer)
        ArtifactManager artifacts =  combatManager.gameObject.GetComponent<ArtifactManager>();
        if (!artifacts.isPlayer2)
        {
            artifacts.CheckOnEnemyDeath(artifacts.ArtifactsListP1);
        }
        else
        {
            artifacts.CheckOnEnemyDeath(artifacts.ArtifactsListP2);
        }

        if (artifacts.isPlayer2)
        {
            artifacts.VagabondKilledThisTurn = true;
        }


        combatManager.CheckGameEnd();
        Destroy(Slider.gameObject);
        Destroy(gameObject);

    }

    private void OnMouseOver()
    {
        manager.CurrentEnemy = this;
    }

    private void OnMouseExit()
    {
        manager.CurrentEnemy = null;
    }


    private void OnDestroy()
    {
        manager.AllEnemies.RemoveAt(manager.AllEnemies.IndexOf(gameObject));
    }


   public void UpdateHealthValue()
    {

        Slider.value = CurrentHealth;
        HealthText.text = CurrentHealth.ToString() + " / " + StartingHealth.ToString();

    }
}
