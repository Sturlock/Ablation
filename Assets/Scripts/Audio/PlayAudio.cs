using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] private Animator ani;
    [SerializeField] private bool Fade;
    [SerializeField] private bool Hit;
    //[SerializeField] private GameObject target;
    [SerializeField] private AudioSource aud;
    public AudioClip playClip;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Fade)
        {
            ani.SetBool("Fade", true);
        }
        else
        {
            ani.SetBool("Fade", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //target = other.gameObject;
            if (!Hit)
            {
                aud.PlayOneShot(playClip);
            }
            Fade = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //target = null;
            Fade = false;
        }
    }
}
