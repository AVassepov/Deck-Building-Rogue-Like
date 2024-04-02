using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    private Deck deck;

    // deal this much extra damage per attack
    public int Strength;

    public int Finnese;

    // at random draw or discard a random card this many times at the start of your turn
    public int Instanity;
    //skip turn
    public int CanAttackStacks;

    public bool Riposting;

    public int RipostingDamage;


    // Can't gain any armor this turn
    public int FearStacks;

    // Gain this amount of strength at the start of your turn
    public int Growth;

    // cant play actions
    public int PanicStacks;

    public int totalDeckRarity;

}
