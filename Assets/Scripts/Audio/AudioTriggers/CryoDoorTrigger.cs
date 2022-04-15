using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryoDoorTrigger : MonoBehaviour
{
    public bool pickup = false;
    public AudioClip audioClip;
    private AudioSource audioSource;
    public Animator animator;

    public void CryoDoorOpen()
    {
        pickup = true;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
        animator.SetBool("Pickup", pickup);
    }
}
