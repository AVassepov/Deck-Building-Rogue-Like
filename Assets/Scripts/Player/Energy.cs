using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public int MaximumEnergy = 10;
    public int CurrentEnergy;

    public TextMesh EnergyText;



    // Start is called before the first frame update
    void Start()
    {
        CurrentEnergy = MaximumEnergy;
        EnergyText.text = CurrentEnergy.ToString();
    }

     public void UpdateEnergy()
    {
        EnergyText.text = CurrentEnergy.ToString();
    }
}
