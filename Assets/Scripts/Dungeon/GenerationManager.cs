using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerationManager : MonoBehaviour
{ 
    //list of all posiblle combinations 
    [Header("Room Generation Options")]
    public List<GameObject> LeftOpening;
    public List<GameObject>RightOpening;
    public List<GameObject>TopOpening;
    public List<GameObject> BottomOpening;
    public List<GameObject> LT;
    public List<GameObject> LB;
    public List<GameObject> RB;
    public List<GameObject> RT;

    //add rooms to this list once all of the rooms are set and their encounters need to be set
    public List<GameObject> Stage2List;

    [Header("Other")]
    public GameObject StartingRoom;
    public GameObject Stage3;
    public int Min, Max;
    public List<GameObject> RoomList;
    [SerializeField]private  DungeonNavigation Navigation;

    private bool DoReset;
    private bool Resetting;
    private bool finishedReset;
    private bool finishedStage3;
    [HideInInspector]
    public bool finishBuilding;
    [Header("Rests and Shops in level")]
    public int ShopAmount =2;
    public int RestAmount =2;

    public bool CanGenerate = true;

    private bool beganStage3;

    private void Start()
    {
        Invoke("CheckList", 1f);
        Navigation = FindObjectOfType<DungeonNavigation>();
    }

    private void Update()
    {
        //try generating again if too many or too little rooms
        if (Resetting &&  DoReset )
        {
            ResetRooms();
        }

        if(finishedReset && !finishedStage3 && !beganStage3)
        {

           
                BeginStage3();
            beganStage3=true;
        }

        if(RoomList.Count> Max && !beganStage3)
        {
            CanGenerate = false;
            BeginStage3();
            finishBuilding = true;
            beganStage3 = true;
            // Invoke("ResetRooms", 0.2f);
        }
    }

    private void CheckList()
    {
        int removed = 0;
        int saved = RoomList.Count;

        for (int i = 0; i < saved; i++)
        {
            if (RoomList[i] != null && RoomList[i].GetComponent<Room>() == null && RoomList != null) {
                removed++;
            }
            
        }

        if(removed == RoomList.Count)
        {
            Resetting = true;

            if (RoomList.Count < Min || RoomList.Count > Max)
            {
                DoReset = true;


            }
            else
            {
                finishedReset = true;
            }


        }
        else
        {
            Invoke("CheckList", 1f);
        }
    }

    void ResetRooms()
    {

        for (int i = 0; i < RoomList.Count ; i++)
        {
           Destroy(RoomList[0]);
           RoomList.RemoveAt(0);
        }

        for (int i = 0;i < Stage2List.Count;i++)
        {
            Destroy(Stage2List[0]);
            Stage2List.RemoveAt(0);
        }

        Stage2List.Clear();
        print("Finished reset");



        if( RoomList.Count == 0 && StartingRoom.GetComponent<Room>() == null) {
            Room room = StartingRoom.AddComponent<Room>();
            room.Left = true; room.Right = true; room.Top = true; room.Bottom = true; room.IsStarting = true;
            Resetting = false;
            DoReset = false;
            Invoke("CheckList", 1f);
        }

        CanGenerate = true;
    }


    void BeginStage3()
    {
        print("Began stage 3");

        for (int i = 0; i < RoomList.Count; i++)
        {
            BlockExits[] temp = new BlockExits[4];
            temp = RoomList[i].GetComponentsInChildren<BlockExits>();
            for (int j = 0; j < temp.Length; j++)
            {
                temp[j].Block();
            }
        }


        for (int i = 0; i < RoomList.Count; i++)
        {
            RoomList[i].GetComponent<BoxCollider2D>().enabled = false;
        }

        for (int i = 0; i < RoomList.Count; i++)
        {
             GameObject temp= Instantiate(Stage3 , RoomList[i].transform); 

         
        }


        SelectRoomType();
        StartingRoom.GetComponent<BoxCollider2D>().enabled = false;
        Instantiate(Stage3, StartingRoom.transform);



    }


    void SelectRoomType()
    {
        int Fights = (RoomList.Count - ShopAmount - RestAmount - 2) / 2;
        int Events = Fights;
        //Add boss

        RoomList[RoomList.Count - 1].transform.Find("RoomType(Clone)").transform.GetComponent<RoomStage3>().SelectedEncounter = RoomStage3.EncounterType.Boss;


        //Add shops 
        for (int i = 0; i < ShopAmount; i++)
        {
            int index = Random.Range(4, RoomList.Count - 3);

            RoomList[index].transform.Find("RoomType(Clone)").GetComponent<RoomStage3>().SelectedEncounter = RoomStage3.EncounterType.Shop;

        }


        //Add Rests 
        while (RestAmount > 0)
        {
            for (int i = 0; i < RestAmount; i++)
            {
                int index = Random.Range(4, RoomList.Count - 3);
                if (RoomList[index].transform.Find("RoomType(Clone)").GetComponent<RoomStage3>().SelectedEncounter == RoomStage3.EncounterType.Empty)
                {
                    RoomList[index].transform.Find("RoomType(Clone)").GetComponent<RoomStage3>().SelectedEncounter = RoomStage3.EncounterType.Rest;
                    RestAmount--;
                }
            }
        }


        //Add fights
        while (Fights > 0)
        {
            for (int i = 0; i < Fights; i++)
            {
                int index = Random.Range(0, RoomList.Count - 2);
                if (RoomList[index].transform.Find("RoomType(Clone)").GetComponent<RoomStage3>().SelectedEncounter == RoomStage3.EncounterType.Empty)
                {
                    RoomList[index].transform.Find("RoomType(Clone)").GetComponent<RoomStage3>().SelectedEncounter = RoomStage3.EncounterType.Fight;
                    Fights--;
                }
            }
        }


        // Add Random Events

        for (int i = 0; i < RoomList.Count; i++)
        {
            if (Events != 0 && RoomList[i].transform.Find("RoomType(Clone)").GetComponent<RoomStage3>().SelectedEncounter == RoomStage3.EncounterType.Empty)
            {
                RoomList[i].transform.Find("RoomType(Clone)").GetComponent<RoomStage3>().SelectedEncounter = RoomStage3.EncounterType.Event;
                Events--;
            }
        }


        Navigation.CanMove = true;

    }



}
