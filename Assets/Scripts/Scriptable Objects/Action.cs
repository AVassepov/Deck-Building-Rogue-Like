using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Actiom", menuName = "Enemy Action")]
public class Action : ScriptableObject
{
    public string ActionName;

    [Header("Attack")]
    public int NumberOfAttacks;
    public int Damage;
    public bool IsAoe;

    [Header("Buff")]
    public string BuffType;
    public int BuffAmount;



    [Header("Debuff")]
    public string DebuffType;
    public int DebuffAmount;

    [Header("Block")]
    public int BlockAmount;

    [Tooltip("if 0 self, if 1 someone else, if 2 give to all")]
    public int TypeOfBlockGiving;

    

}
