using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayArtifacts : MonoBehaviour
{
    public List<GameObject> ArtifactsP1 ;
    public List<GameObject> ArtifactsP2;

    public List<GameObject> SavedList;

    // Start is called before the first frame update
    void Start()
    {
        SetLocations(ArtifactsP1);
        SavedList = ArtifactsP2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLocations(List<GameObject> ShowThis)
    {
      

        for (int i = 0; i < ShowThis.Count; i++)
        {
            ShowThis[i].transform.position = new Vector3(this.transform.position.x + (i*0.9f), this.transform.position.y, this.transform.position.z);
        }

        if (SavedList == ArtifactsP2)
        {
            SavedList = ArtifactsP1;
        }
        else
        {
            SavedList = ArtifactsP2;
        }

        for (int i = 0; i < SavedList.Count; i++)
        {
          SavedList[i].transform.position = new Vector3(1000,1000,1000);
        }

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Pressed");

            SetLocations(SavedList);

          
        }
    }
}
