using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{

    public bool Right, Left,Top, Bottom;

    private bool spawned;

    private GenerationManager generationManager;
    public bool Occupied;
    private void Awake()
    {
        generationManager = FindObjectOfType<GenerationManager>();
    }

    private void Start()
    {
        if(!Occupied)
        {
            Invoke("Spawn", 0.5f);
        }


    }



    private void Spawn ()
    {
        if (!spawned) { 
        if (Left)
        {
            List<GameObject> list = generationManager.LeftOpening;

            int index = Random.Range(0, list.Count);

            GameObject Temp;
            Temp = Instantiate((list[index]), transform);
            Left = false;
        }

        if (Right)
        {
            List<GameObject> list = generationManager.RightOpening;

            int index = Random.Range(0, list.Count);

            GameObject Temp;
            Temp = Instantiate((list[index]), transform);
            Right = false;
        }

        if (Top)
        {
            List<GameObject> list = generationManager.TopOpening;

            int index = Random.Range(0, list.Count);

            GameObject Temp;
            Temp = Instantiate((list[index]), transform);
            Top = false;
        }
        if (Bottom)
        {
            List<GameObject> list = generationManager.BottomOpening;

            int index = Random.Range(0, list.Count);

            GameObject Temp;
            Temp = Instantiate((list[index]), transform);
            Bottom = false;
        }

            spawned = true;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Occupied = true;
        print("CANT SPAWN HERE");
    }
}
