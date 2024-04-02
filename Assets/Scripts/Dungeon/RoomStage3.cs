using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class RoomStage3 : MonoBehaviour
{
    public List<GameObject> Neighbours;
    [SerializeField] private List<Sprite> Icons;

    private DungeonNavigation Navigation;
    public bool WasCleared;
    private ContestedRooms contested;
    public LayerMask IgnoreMe;
    public enum EncounterType // your custom enumeration
    {
        Empty,
        Event,
        Shop,
        Fight,
        Boss,
        Rest
    };

    public EncounterType SelectedEncounter;
    private void Awake()
    {
        contested = FindObjectOfType<ContestedRooms>();
        Navigation = FindObjectOfType<DungeonNavigation>();
    }
    private void Start()
    {
        Invoke("DetectNeighbours", 2f);


    }


    public void DetectNeighbours()
    {
        //  Physics2D.OverlapBox ( transform.position + Vector3.up * 5 , new Vector3(1,1,1) , 0); use this collision based neighbour detection if raycasts break again

        //up
        RaycastHit2D up = Physics2D.Raycast(transform.position + new Vector3 (2,0,0), transform.position + Vector3.up * 5,  ~IgnoreMe);
        Debug.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up * 5);
        if (Physics2D.Raycast(transform.position + new Vector3(2, 0, 0), transform.position + Vector3.up * 5, ~IgnoreMe) && up.transform.gameObject.GetComponent<RoomStage3>() != null  )
        {
            Neighbours.Add(up.transform.gameObject);
        }

        //down
        RaycastHit2D down = Physics2D.Raycast(transform.position + Vector3.down, transform.position + Vector3.down * 5,~IgnoreMe);
        Debug.DrawLine(transform.position + Vector3.down, transform.position + Vector3.down * 5);
        if (Physics2D.Raycast(transform.position + Vector3.down, transform.position + Vector3.down * 5, ~IgnoreMe) && down.transform.gameObject.GetComponent<RoomStage3>() != null )
        {
            Neighbours.Add(down.transform.gameObject);
        }

        //left
        RaycastHit2D left = Physics2D.Raycast(transform.position + Vector3.left, transform.position + Vector3.left * 5, ~IgnoreMe);
        Debug.DrawLine(transform.position + Vector3.left, transform.position + Vector3.left * 5);
        if (Physics2D.Raycast(transform.position + Vector3.left, transform.position + Vector3.left * 5, ~IgnoreMe) && left.transform.gameObject.GetComponent<RoomStage3>() != null )
        {
            Neighbours.Add(left.transform.gameObject);
        }
        //right
        RaycastHit2D right = Physics2D.Raycast(transform.position + Vector3.right, transform.position + Vector3.right * 5, ~IgnoreMe);
        Debug.DrawLine(transform.position + Vector3.right, transform.position + Vector3.right * 5);
        if (Physics2D.Raycast(transform.position + Vector3.right, transform.position + Vector3.right * 5, ~IgnoreMe) && right.transform.gameObject.GetComponent<RoomStage3>() != null)
        {
            Neighbours.Add(right.transform.gameObject);
        }
        Invoke("AddRestsNearBoss", 0.1f);
    }

    private void ShowIcon()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Icons[(int)SelectedEncounter];

    }

    private void AddRestsNearBoss()
    {
        if (SelectedEncounter == EncounterType.Boss)
        {
            for (int i = 0; i < Neighbours.Count; i++)
            {
                if (Neighbours[i].GetComponent<RoomStage3>().SelectedEncounter != EncounterType.Boss)
                {
                    Neighbours[i].GetComponent<RoomStage3>().SelectedEncounter = EncounterType.Rest;
                }
            }
            SelectedEncounter = EncounterType.Boss;
        }

        Invoke("ShowIcon", 0.1f);
    }

    public void ClearRoom()
    {
        WasCleared = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = Icons[0];

        Navigation.CanMove = true ;
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision!=null && collision.GetComponent<RoomStage3>()!=null)
        {
          contested.rooms.Add(transform.parent.gameObject);
        }
    }
}
