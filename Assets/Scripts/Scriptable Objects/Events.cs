using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Events : ScriptableObject
{
    public Sprite Image;

    public string Title;


    [TextArea]
    public string Description;


    [TextArea]
    public string[] OptionNames;

    public options[] OptionOutcomes;

    public CombatEncounter SpecifiedFight;

    public List<GameObject> PossibleArtifacts;

    public enum options
    {
        Leave,
        Fight,
       GetItem,
       GetResource,
       WildCard

    }
}

