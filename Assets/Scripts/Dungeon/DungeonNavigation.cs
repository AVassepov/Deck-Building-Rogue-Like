using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DungeonNavigation : MonoBehaviour
{
    public RoomStage3 currentRoom;
    public bool CanMove;
    public EncounterManager EncounterManager;

    private Vector2 saved;
    private void Start()
    {
        EncounterManager = GameObject.Find("Managers").GetComponent<EncounterManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("down")&& CanMove)
        {
            Move(new Vector2(0,-1));
            saved = new Vector2(0, -1);
        }
        if (Input.GetKeyDown("up") && CanMove)
        {
            Move(new Vector2(0, 1));
            saved = new Vector2(0, 1);
        }
        if (Input.GetKeyDown("left") && CanMove)
        {
            Move(new Vector2(-1, 0));
            saved = new Vector2(-1,0);
        }
        if (Input.GetKeyDown("right") && CanMove)
        {
            Move(new Vector2(1, 0));
            saved = new Vector2(1,0);
        }



    }

    private void Move(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + direction, direction);
        if (hit && hit.collider.gameObject.GetComponent<RoomStage3>() != null)
        {
            transform.position = hit.transform.position;
        }

        if (hit && hit.transform.gameObject.GetComponentInChildren<RoomStage3>().SelectedEncounter == RoomStage3.EncounterType.Fight)
        {
            EncounterManager.CurrentRoom = (hit.transform.gameObject.GetComponentInChildren<RoomStage3>());
            EncounterManager.StartCombat(EncounterManager.ListOfCombat[EncounterManager.CurrentFloor]);
            if (!EncounterManager.CurrentRoom.WasCleared)
            {
                CanMove = false;
            }
        }
        if (hit.transform.gameObject.GetComponentInChildren<RoomStage3>().SelectedEncounter == RoomStage3.EncounterType.Event)
        {
            EncounterManager.CurrentRoom = (hit.transform.gameObject.GetComponentInChildren<RoomStage3>());
            EncounterManager.StartEvent(EncounterManager.ListOfEvents[EncounterManager.CurrentFloor]);
            CanMove = false;
            if (EncounterManager.CurrentRoom.WasCleared)
            {
                CanMove = true;
            }
        }
        if (hit.transform.gameObject.GetComponentInChildren<RoomStage3>().SelectedEncounter == RoomStage3.EncounterType.Rest)
        {
            EncounterManager.CurrentRoom = (hit.transform.gameObject.GetComponentInChildren<RoomStage3>());
            EncounterManager.StartResting();
            CanMove = false;
            if (EncounterManager.CurrentRoom.WasCleared)
            {
                CanMove = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<RoomStage3>() != null)
        {
            currentRoom = collision.GetComponent<RoomStage3>();
        }
    }

}
