using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTrigger : MonoBehaviour
{
    public bool grab = false;
    public float timer = 0.0f;

    private Vector2 ogScale;
    private Vector2 newScale;
    void Update()
    {
        if (grab)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector2.Lerp(ogScale, newScale, timer);
        }

        if(timer >= 1)
        {
            grab = false;
        }
        
    }

    public void Grab()
    {
        grab = true;
        ogScale = transform.localScale;
        newScale = ogScale + new Vector2(1, 1);
    }
    public void OnClick()
    {
        gameObject.SetActive(false);
    }
}
