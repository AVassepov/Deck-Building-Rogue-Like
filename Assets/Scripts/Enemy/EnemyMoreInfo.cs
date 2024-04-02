using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyMoreInfo : MonoBehaviour
{

    [SerializeField] private TextMesh Name;

    private bool left;
    private bool notShown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }


    private void OnMouseOver()
    {
        if (!notShown)
        {
            StartCoroutine(FadeTextToFullAlpha(1f, Name));
        }
        
    }


       // Name.color = Color.Lerp(new Color(256, 256, 256, 0), new Color(256, 256, 256, 256),1f);


    void OnMouseExit()
    {
        StopAllCoroutines();
        StartCoroutine(FadeTextToZeroAlpha(0.3f, Name));
        notShown = false;
    }

    public IEnumerator FadeTextToFullAlpha(float t, TextMesh i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        notShown = true;
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMesh i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
