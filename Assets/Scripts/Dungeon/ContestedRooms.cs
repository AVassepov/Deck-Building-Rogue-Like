using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContestedRooms : MonoBehaviour
{
    public List<GameObject> rooms;

    private GenerationManager generationManager;

    private void Awake()
    {
        generationManager = gameObject.GetComponent<GenerationManager>();
    }

    void Update()
    {
        if (rooms.Count > 1) {
            Contest();
      }


    }



    private void Contest()
    {

        for (int i = 0; i < rooms.Count; i++)
        {
            //if(i%2 == 0)
           // {
                Destroy(rooms[0]);

           // }
        }
        rooms.Clear();
    }
}
