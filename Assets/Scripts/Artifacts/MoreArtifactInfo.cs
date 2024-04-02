using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoreArtifactInfo : MonoBehaviour
{
    public ArtifactInfo info;



    private void Start()
    {

        transform.GetChild(0).GetComponent<TextMesh>().text = info.Name;
        transform.GetChild(1).GetComponent<TextMesh>().text = info.rarity.ToString();
        transform.GetChild(2).GetComponent<TextMesh>().text = info.Description;
        transform.GetChild(3).GetComponent<TextMesh>().text = info.FlavourText;
        transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = info.Image;


        if (info.rarity == ArtifactInfo.Rarity.Starter)
        {
            transform.GetChild(1).GetComponent<TextMesh>().color = Color.grey;
        }else if (info.rarity == ArtifactInfo.Rarity.Common)
        {
            transform.GetChild(1).GetComponent<TextMesh>().color = Color.black;
        }
        else if (info.rarity == ArtifactInfo.Rarity.Rare)
        {
            transform.GetChild(1).GetComponent<TextMesh>().color = Color.blue;
        }
        else if (info.rarity == ArtifactInfo.Rarity.Legendary)
        {
            transform.GetChild(1).GetComponent<TextMesh>().color = new Color32 (198, 115, 255 , 255);
        }
        else if (info.rarity == ArtifactInfo.Rarity.Boss)
        {
            transform.GetChild(1).GetComponent<TextMesh>().color = Color.red;
        }
        else if (info.rarity == ArtifactInfo.Rarity.Shop)
        {
            transform.GetChild(1).GetComponent<TextMesh>().color = new Color32(255, 216, 1, 255);
        }
        else if (info.rarity == ArtifactInfo.Rarity.Event)
        {
            transform.GetChild(1).GetComponent<TextMesh>().color = new Color32(75, 0, 110, 255);
        }
    }
}
