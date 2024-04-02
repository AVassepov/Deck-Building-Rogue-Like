using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public int CurrentHP;
    public int MaxHP;
    public int Block;




    private SelectPlayer manager;

    [SerializeField] private Slider Slider;
    public TextMesh HealthText;


    private StatusEffects statusEffects;

    private void Start()
    {
        statusEffects = gameObject.GetComponent<StatusEffects>();
        HealthText.text = CurrentHP + " / " + MaxHP;

        manager = GameObject.Find("Managers").GetComponent<SelectPlayer>();
    }


    private void OnMouseOver()
    {
        manager.CurrentPlayerHealth = this;
    }

    private void OnMouseExit()
    {
        manager.CurrentPlayerHealth = null;
    }




    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;

        if(CurrentHP < 0)
        {
            CurrentHP = 0; 
        }

        HealthText.text = CurrentHP + " / " + MaxHP;


        Slider.value = CurrentHP;
    }

    public void Heal(int Healing)
    {
        CurrentHP += Healing;

        if(CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }


        Slider.value = CurrentHP;

        HealthText.text = CurrentHP + " / " + MaxHP;
    }

    public void GainBlock(int block)
    {
        if (statusEffects.FearStacks < 1)
        {
            Block += block;

            HealthText.text = CurrentHP + " / " + MaxHP + " + " + Block;
        }
        else
        {
            HealthText.text = CurrentHP + " / " + MaxHP + " + " + Block;
        }
    }

    public void RemoveBlock()
    {

        Block = 0;
        HealthText.text = CurrentHP + " / " + MaxHP;
    }



}
