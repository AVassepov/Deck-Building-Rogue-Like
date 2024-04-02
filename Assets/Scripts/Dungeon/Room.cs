using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
   


    public bool Right;
    public bool Left;
    public bool Top;
    public bool Bottom;

    public bool IsStarting;
    [SerializeField] private ContestedRooms contested ;
    [SerializeField] private GameObject Wall;
    private GenerationManager generationManager;
    private int frameCounter = 0;


    private bool Done;
    void Awake()
    {
        generationManager = FindObjectOfType<GenerationManager>();
        contested = FindObjectOfType<ContestedRooms>(); 
    }


    private void Start()
    {
        //set this room as starting room
        if (IsStarting)
        {
            generationManager.StartingRoom = gameObject;
        }
        else
        {
            generationManager.RoomList.Add(gameObject);
        }

        transform.parent = GameObject.Find("Generated Dungeon").transform;
    }
    private void Update()
    {
        //Invoke("Spawn", 0.04f);

        if(frameCounter == 10 && generationManager.CanGenerate)
        {
            Spawn();
        }
        else
        {
            frameCounter++;
        }

        if(generationManager.finishBuilding && !Done)
        {
            BlockExits();
            Done=true;
            //Destroy(this);
        }
    }

    // spawn rooms in adjacent empty spots
    void Spawn()
    {
        if (Right )
            {
                List<GameObject> list = generationManager.RightOpening;

                int index = Random.Range(0, list.Count);

                if (list[index].GetComponent<Room>().Left)
                {
                    GameObject Temp;
                    Temp = Instantiate((list[index]), new Vector3(transform.position.x + 5f, transform.position.y, 0), Quaternion.identity);
                    Temp.GetComponent<Room>().Left = false;

                    Right = false;
                }
            }


            if (Left)
            {
                List<GameObject> list = generationManager.LeftOpening;

                int index = Random.Range(0, list.Count);

                if (list[index].GetComponent<Room>().Right)
                {
                    GameObject Temp;
                    Temp = Instantiate((list[index]), new Vector3(transform.position.x - 5f, transform.position.y, 0), Quaternion.identity);
                    Temp.GetComponent<Room>().Right = false;

                    Left = false;
                }
            }


            if (Top )
            {
                List<GameObject> list = generationManager.TopOpening;

                int index = Random.Range(0, list.Count);

                if (list[index].GetComponent<Room>().Bottom)
                {
                    GameObject Temp;
                    Temp = Instantiate((list[index]), new Vector3(transform.position.x, transform.position.y + 5f, 0), Quaternion.identity);
                    Temp.GetComponent<Room>().Bottom = false;

                    Top = false;
                }
            }


            if (Bottom )
            {
                List<GameObject> list = generationManager.BottomOpening;

                int index = Random.Range(0, list.Count);

                if (list[index].GetComponent<Room>().Top)
                {
                    GameObject Temp;
                    Temp = Instantiate((list[index]), new Vector3(transform.position.x, transform.position.y - 5f, 0), Quaternion.identity);
                    Temp.GetComponent<Room>().Top = false;
                    
                    Bottom = false;
                }
            }

            if(!Right && !Left && !Bottom && !Top)
        {
       
            Destroy(this);
        }

     }

    // block out open exits that lead to no rooms
    private void BlockExits()
    {
        if (Left)
        {
            Instantiate(Wall , new Vector3(transform.position.x - 2f, transform.position.y , 0), Quaternion.identity , null);
            Left = false;
        }
        if (Right)
        {
            Instantiate(Wall, new Vector3(transform.position.x +2f, transform.position.y , 0), Quaternion.identity, null);
            Right = false;
        }
        if (Bottom)
        {
            Instantiate(Wall, new Vector3(transform.position.x, transform.position.y - 2f, 0), Quaternion.identity, null);
            Bottom = false;
        }
        if (Top)
        {
            Instantiate(Wall, new Vector3(transform.position.x, transform.position.y + 2f, 0), Quaternion.identity, null);
            Top = false;
        }

    }
}
