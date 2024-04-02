using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStage2 : MonoBehaviour
{

    [SerializeField] private ContestedRooms contested;
    private GenerationManager generationManager;


    public bool Right;
    public bool Left;
    public bool Top;
    public bool Bottom;

    private int SelfDestructCounter;


    private List<GameObject> list = new List<GameObject>();

    private void Awake()
    {
        contested = FindObjectOfType<ContestedRooms>();
        generationManager = FindObjectOfType<GenerationManager>();
        Invoke("CheckThatIExist", 0.3f);
        generationManager.Stage2List.Add(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        Invoke("MakeCorner", 0.35f);

        Invoke("SelfDestruct", 1f);
    }

    // Update is called once per frame
    void Update()
    {

        if (!generationManager.CanGenerate)
        {
            CancelInvoke();
        }

    }


    void CheckTop()
    {
        Collider2D hitColliders = Physics2D.OverlapCircle(transform.position + new Vector3(-5, 0, 0), 1f);
        if (hitColliders != null && hitColliders.tag == "Room")
        {
            Top = false;
        }
        else
        {
            Top= true;
        }
    }
    void CheckBottom()
    {
        Collider2D hitColliders = Physics2D.OverlapCircle(transform.position + new Vector3(-5, 0, 0), 1f);
        if (hitColliders != null && hitColliders.tag == "Room")
        {
            Bottom = false;
        }
        else
        {
            Bottom = true;
        }
    }
    void CheckLeft()
    {
        Collider2D hitColliders = Physics2D.OverlapCircle(transform.position + new Vector3(-5, 0, 0), 1f);
        if (hitColliders!= null && hitColliders.tag == "Room")
        {
            Left = false;
        }
        else
        {
            Left = true;
        }
    }
    void CheckRight()
    {
        Collider2D hitColliders = Physics2D.OverlapCircle(transform.position + new Vector3(-5, 0, 0), 1f);
        if (hitColliders != null && hitColliders.tag == "Room")
        {
            Right = false;
        }
        else
        {
            Right = true;
        }
    }

    private void MakeCorner()
    {
        CheckRight();
        CheckBottom();
        CheckLeft();
        CheckTop();

        if(Top&&Right) { list = generationManager.RT; }
       else if (Top && Left) { list = generationManager.LT; }
        else if (Bottom && Right) { list = generationManager.RB; }
        else if (Bottom && Left) { list = generationManager.LB; }

     int index = Random.Range(0, list.Count);

       if(index < 0)
        {
            index = 0;
        }
       if(index > list.Count)
        {
            index = list.Count;
        }

       if(index!=0 || index> list.Count ) { 
        GameObject Temp = Instantiate((list[index]), transform.position, Quaternion.identity);
        }

        Destroy(gameObject);

    }

    private void CheckThatIExist()
    {

        contested.rooms.Add(gameObject);


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Room")
        {
            CancelInvoke();
            Destroy(gameObject);
        }
    }

    private void SelfDestruct()
    {

    }



}
