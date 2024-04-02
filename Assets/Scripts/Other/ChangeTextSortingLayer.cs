using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTextSortingLayer : MonoBehaviour
{
    [SerializeField] private string LayerName;

    private void Awake()
    {
        Renderer text = this.gameObject.GetComponent<Renderer>();

        text.sortingLayerName = LayerName;
    }




}
