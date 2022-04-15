using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{
    public Animator Loading;
    
    public void Triggered()
    {
        Loading.SetBool("Loading", true);
    }
}
