using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTrigger : MonoBehaviour
{
    public GameObject light_04;

    public void Grab()
    {
        light_04.SetActive(true);
    }

    public void Poof()
    {
        light_04.SetActive(false);
    }
}
