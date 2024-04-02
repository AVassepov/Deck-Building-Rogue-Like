using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    private PlayerHealth SelectedPlayer;

    public List<Action> Actions;

    public Action SelectedAction;

    public List<string> ActionNames;

    public bool CanAttack = true;

    public StatusEffects statusEffects;

    private TurnManager turnManager;

    public int Strength;

    [SerializeField] private List<PlayerHealth> bothPlayers;


    private void Start()
    {
        turnManager = GameObject.Find("Managers").GetComponent<TurnManager>();
        bothPlayers.Add(turnManager.health);
        bothPlayers.Add(turnManager.health2);
        ChooseTarget();
        SelectRandomAction();

    }

    public void Attack(PlayerHealth health)
    {
        if (CanAttack)
        {
            for (int i = 0; i < SelectedAction.NumberOfAttacks; i++)
            {
                int DealThis = SelectedAction.Damage + Strength;

                if (health.Block > 0)
                {
                    int RemainingBlock =0;
                   

                    health.Block -= DealThis;

                    if(health.Block > 0)
                    {
                        health.HealthText.text = health.CurrentHP + " / " + health.MaxHP + " + " + health.Block;


                        if (statusEffects.Riposting)
                           {
                            gameObject.GetComponent<EnemyHealth>().CurrentHealth -= statusEffects.RipostingDamage;

                        }

                    }
                    else if (health.Block == 0 &&statusEffects.Riposting)
                    {
                            gameObject.GetComponent<EnemyHealth>().CurrentHealth -= statusEffects.RipostingDamage;
                       
                    }
                    else
                    {

                        RemainingBlock = -health.Block;
                        health.Block = 0;
                        health.HealthText.text = health.CurrentHP + " / " + health.MaxHP;
                             health.TakeDamage(RemainingBlock);

                    }

                 

                }
                else
                {

                    health.TakeDamage(DealThis);
                }

            }
        }
        else
        {

            CanAttack = true;
        }
        ChooseTarget();
    }

    public void PlayAction()
    {
        if (SelectedAction.ActionName == "Attack" && !SelectedAction.IsAoe)
        {
            Attack(SelectedPlayer);
            SelectRandomAction();
        }
        else if (SelectedAction.ActionName == "Attack" && SelectedAction.IsAoe)
        {
            Attack(bothPlayers[0]);
            Attack(bothPlayers[1]);
            SelectRandomAction();
        }




        }

        public void SelectRandomAction()
    {
        int i = Random.Range(0, Actions.Count);


            SelectedAction = Actions[i];
        

    }


    public void ChooseTarget()
    {
        int index = Random.Range(0, 2);

        print(index);

        SelectedPlayer = bothPlayers[index];

        if(SelectedPlayer.CurrentHP == 0 && index == 1)
        {
            SelectedPlayer = bothPlayers[0];
        }else if (SelectedPlayer.CurrentHP == 0 && index == 0)
        {
            SelectedPlayer = bothPlayers[1];
        }

        statusEffects = SelectedPlayer.gameObject.GetComponent<StatusEffects>();


    }
}
