using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Artifact", menuName = "Artifacts")]
public class ArtifactInfo : ScriptableObject
{
    public Sprite Image;

    public string Name;
    [TextArea]
    public string Description;
    [TextArea]
    public string FlavourText;

    public enum Rarity
    {
        Starter,
        Common,
        Rare,
        Legendary,
        Boss,
        Shop , 
        Event

    }

    public Rarity rarity;

   


}
