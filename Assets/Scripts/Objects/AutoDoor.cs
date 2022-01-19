using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    Animator ani;
    void Awake()
    {
        ani = GetComponentInChildren<Animator>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            ani.SetBool("Open", true);
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            ani.SetBool("Open", false);
        }
    }
}