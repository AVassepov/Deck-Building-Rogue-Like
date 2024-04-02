using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelAction : MonoBehaviour
{

    public GameObject DeletedUI;


    private void Start()
    {
        if (DeletedUI != null)
        {
            DeletedUI = transform.parent.gameObject;
        }
    }


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(DeletedUI);

        }

        }
}
