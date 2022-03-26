using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTrigger : MonoBehaviour
{
    public bool grab = false;
    public float timer = 0.0f;

    private Vector3 ogScale;
    private Vector3 newScale;
    void Update()
    {
        if (grab)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(ogScale, newScale, timer);
     
        }

        if(timer >= 1)
        {
            grab = false;
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        
    }

    public void Grab()
    {
        grab = true;
        ogScale = transform.localScale;
        newScale = ogScale + new Vector3(1, 1, 1);
    }

    public void Poof()
    {
        gameObject.SetActive(false);
    }

    public void OnClick()
    {
        gameObject.SetActive(false);
    }
}
