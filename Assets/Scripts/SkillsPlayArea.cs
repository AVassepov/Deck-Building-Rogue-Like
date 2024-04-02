using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillsPlayArea : MonoBehaviour
{
    public bool NotAimingAtHand;
    public void OnMouseOver()
    {
        NotAimingAtHand = true;
    }

    public void OnMouseExit()
    {
        NotAimingAtHand = false;
    }
}
