using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TopSortingLayer : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer Image;
    [HideInInspector]
    public TextMesh Name;
    [HideInInspector]
    public TextMesh Description;
    [HideInInspector]
    public TextMesh EnergyCost;

    private Vector3 SavedScale;
    [HideInInspector]
    public SpriteRenderer cardRim;
    [HideInInspector]
    public SpriteRenderer EnergyIcon;

    [HideInInspector]
    public CardInfo CardInfo;
    private void Awake()
    {
        cardRim = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        Image = transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        EnergyIcon = transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();
        EnergyCost= transform.GetChild(2).GetChild(0).gameObject.GetComponent<TextMesh>();
        Name = transform.GetChild(3).gameObject.GetComponent<TextMesh>();
        Description = transform.GetChild(4).gameObject.GetComponent<TextMesh>();
    }



    private void LateUpdate()
    {
        Destroy(gameObject.GetComponent<ApplyDataToCard>());


        EnergyCost.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Show Pile";
        Description.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Show Pile";
        Name.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Show Pile";

        cardRim.sortingLayerName = "Show Pile";
        EnergyIcon.sortingLayerName = "Show Pile";
        Image.sortingLayerName = "Show Pile";



        Destroy(this);
    }


}
