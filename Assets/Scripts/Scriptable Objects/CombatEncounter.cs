using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combat Encounter", menuName = "Combat Encounter")]
public class CombatEncounter : ScriptableObject
{
   public List<GameObject> enemies;

   public bool IsBoss;
   public bool IsElite;
}
