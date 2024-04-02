using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card" , menuName="Card")]
public class CardInfo : ScriptableObject
{
    public Sprite Image;

    public string Name;
    [TextArea]
    public string Description;

    public int EnergyCost;

    public int DamageOrBlock;

    //-2 starting , 0 common, 1 rare , 3 legendary
    public int Rarity;

    public string Type;
}
