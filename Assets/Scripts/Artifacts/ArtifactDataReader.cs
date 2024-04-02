using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactDataReader : MonoBehaviour
{
    public ArtifactInfo ArtifactInfo;
    [SerializeField] private TextMesh Name;



    [HideInInspector]
    public SpriteRenderer Image;


    // Start is called before the first frame update
    void Start()
    {
        Image = GetComponent<SpriteRenderer>();
        Name = GetComponentInChildren<TextMesh>();


        Image.sprite = ArtifactInfo.Image;

        if(Name!= null) { 
        Name.text=ArtifactInfo.Name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
