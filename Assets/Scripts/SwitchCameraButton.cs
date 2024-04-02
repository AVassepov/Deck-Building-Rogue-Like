using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraButton : MonoBehaviour
{
    [SerializeField] private GameObject SwitchOn;
    [SerializeField] private GameObject SwitchOff;
    [SerializeField] private GameObject SwitchOff2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Switch(SwitchOn,SwitchOff,SwitchOff2);
        }

    }




    private void Switch(GameObject TurnedOn, GameObject TurnedOff, GameObject TurnedOff2)
    {
        TurnedOff.SetActive(false);
        TurnedOff2.SetActive(false);
        TurnedOn.SetActive(true);



    }
}
