using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutters : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        animator.SetTrigger("Open");
    }
}
