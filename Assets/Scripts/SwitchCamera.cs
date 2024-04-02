using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private GameObject FightCam;
    [SerializeField] private GameObject DeckCam;
    [SerializeField] private GameObject CoinPouchCam;
    void Start()
    {
        DeckCam.SetActive(false);
        CoinPouchCam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            Switch(CoinPouchCam, FightCam, DeckCam);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Switch(FightCam, CoinPouchCam, DeckCam);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Switch(DeckCam,FightCam, CoinPouchCam);

        }


    }

    public void Switch(GameObject TurnedOn, GameObject TurnedOff, GameObject TurnedOff2){
        TurnedOff.SetActive(false);
        TurnedOff2.SetActive(false);
        TurnedOn.SetActive(true);

      

    }



}
