using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ApplyDataToCard : MonoBehaviour
{
    public CardInfo CardInfo;

    [HideInInspector]
    public SpriteRenderer Image;
    [HideInInspector]
    public TextMesh Name;
    [HideInInspector]
    public TextMesh Description;
    [HideInInspector]
    public TextMesh EnergyCost;

    private TurnManager TurnManager;

    [SerializeField] private Vector3 SavedScale;
    [HideInInspector]
    public SpriteRenderer cardRim;
    [HideInInspector]
    public SpriteRenderer EnergyIcon;
    // Start is called before the first frame update


    private Rest rest;
    private Card card;
    public bool isUpgrading;

    public int defaultLayer;
    void Awake ()
    {
        TurnManager = FindObjectOfType<TurnManager>();

        cardRim = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        Image = transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        EnergyIcon = transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();
        EnergyCost = transform.GetChild(2).GetChild(0).gameObject.GetComponent<TextMesh>();
        Name = transform.GetChild(3).gameObject.GetComponent<TextMesh>();
        Description = transform.GetChild(4).gameObject.GetComponent<TextMesh>();
        card = gameObject.GetComponent<Card>();


        Image.sprite = CardInfo.Image;
        Name.text = CardInfo.Name;
        EnergyCost.text = CardInfo.EnergyCost.ToString();

        if (isUpgrading)
        {
            SavedScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else
        {
            SavedScale = new Vector3(2,2,2);
        }
        ResetLayer();

        string savedDescription = CardInfo.Description;

        if (savedDescription.Contains("(DAMAGE)"))
        {
            //print("card deals damage");
            if (CardInfo != null) { 
            int temp =TurnManager.CurrentStatusEffects.Strength + CardInfo.DamageOrBlock;
          
            if(CardInfo.Name == "Big Sword")
            {
                temp = TurnManager.CurrentStatusEffects.Strength * 2 + CardInfo.DamageOrBlock;
            }
            if (temp < 0)
            {
                temp = 0;
            }

            savedDescription= savedDescription.Replace("(DAMAGE)", temp.ToString());
            }
        }

       Description.text = savedDescription;
    }

    private void Start()
    {
        if (isUpgrading)
        {
            rest = GameObject.FindObjectOfType<Rest>();
        }
        SavedScale = transform.localScale;

        if (isUpgrading)
        {
            SavedScale = new Vector3(1.5f, 1.5f, 1.5f);
            transform.localScale = SavedScale;
        }
    }


    private void OnMouseOver()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isUpgrading) {

            transform.localScale = new Vector3(3, 3, 3);
            EnergyCost.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Inspecting Card";
            Description.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Inspecting Card";
            Name.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Inspecting Card";

            cardRim.sortingLayerName = "Inspecting Card";
            EnergyIcon.sortingLayerName = "Inspecting Card";
            Image.sortingLayerName = "Inspecting Card";
        }
        else if (!isUpgrading)
        {
            ResetLayer();
        }

        if (Input.GetMouseButtonDown(0) && isUpgrading)
        {
            rest.UpgradeCard(this);

        }
    }
        private void OnMouseExit()
    {
        if (!isUpgrading) { 
        ResetLayer();
        }
    }

    private void ResetLayer()
    {

        transform.localScale = SavedScale;
        EnergyCost.gameObject.GetComponent<MeshRenderer>().sortingLayerName = defaultLayer.ToString();
        Description.gameObject.GetComponent<MeshRenderer>().sortingLayerName = defaultLayer.ToString();
        Name.gameObject.GetComponent<MeshRenderer>().sortingLayerName = defaultLayer.ToString();

        cardRim.sortingLayerName = defaultLayer.ToString(); 
        EnergyIcon.sortingLayerName = defaultLayer.ToString();
        Image.sortingLayerName = defaultLayer.ToString();

    }

    public void ResetLayerForUpgrades()
    {

        transform.localScale = SavedScale;
        EnergyCost.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Top of UI";
        Description.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Top of UI";
        Name.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Top of UI";

        cardRim.sortingLayerName = "Top of UI";
        EnergyIcon.sortingLayerName = "Top of UI";
        Image.sortingLayerName = "Top of UI";
    }
}
