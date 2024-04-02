using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighbourDetector : MonoBehaviour
{
    public bool Left, Right ,Top, Down;

    [SerializeField] private GameObject roomStage2;
    private Room room;
    private GenerationManager generationManager;

    private void Start()
    {
        room = transform.parent.GetComponent<Room>();
        generationManager = FindObjectOfType<GenerationManager>();
    }

    private void Update()
    {
            if(room == null)
        {
            Instantiate(roomStage2, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Room") { 

        if (Top)
            {
                if (room != null)
                {
                    room.Top = false;
           //     print("Blocked Top");

                    room.CancelInvoke();
                }


            }
            if (Left)
            {
                if (room != null)
                {
                    room.Left = false;
             //   print("Blocked left");
                room.CancelInvoke();
                }

            }
            if (Right)
            {
                if (room != null)
                {
                    room.Right = false;
             //   print("Blocked right");
                room.CancelInvoke();
                }

            }
        if (Down)
        {
            if (room != null)
            {
                room.Bottom = false;
                //   print("Blocked Down");
                room.CancelInvoke();
            }
        }
        }
    }


}


   
