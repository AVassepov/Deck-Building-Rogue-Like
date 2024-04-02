using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseArtifactWindow : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Destroy(transform.parent.gameObject);

        }

    }
}
