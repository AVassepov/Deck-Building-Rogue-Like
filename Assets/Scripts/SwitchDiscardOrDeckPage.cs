using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDiscardOrDeckPage : MonoBehaviour
{
    public ShowCardPiles ShowPile;
    [SerializeField]private  int Value;
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShowPile.ChangePage(Value);

        }
    }
}
