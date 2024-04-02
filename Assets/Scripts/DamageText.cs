using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private TextMesh TextMesh;
    [SerializeField] private float sizeLimit = 2.5f;

    private float yLimit = 0.05f;
    private float x,y;

    private int selfDestructTimer;

    private bool ForceAdded;

    // Start is called before the first frame update
    void Awake()
    {
        TextMesh = gameObject.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayAnimation();
    }



    private void PlayAnimation()
    {
        if (y < yLimit)
        {
            y += 0.001f;
            x -= 0.002f;
             transform.position = transform.position + new Vector3(x, y, 0);

            //GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y));

        }
        else if (!ForceAdded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0,100));
        ForceAdded = true;
        }
        
        
        if(selfDestructTimer<300)
        {
            selfDestructTimer++;
        }
        else { 
        Destroy(gameObject);
        }

        TextMesh.color = Color.Lerp(TextMesh.color,new Color32(110, 38, 14, 255)  ,0.015f);
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(sizeLimit, sizeLimit, sizeLimit), 0.015f);
    
    
    
    }

}
