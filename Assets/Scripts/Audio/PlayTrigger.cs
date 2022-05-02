using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTrigger : MonoBehaviour
{
    public bool Played = false;
    [SerializeField] private bool Hit;
    [SerializeField] private AudioSource aud;
    public AudioClip playClip;
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !Played)
        {
            //target = other.gameObject;
            if (!Hit)
            {
                aud.PlayOneShot(playClip);
            }
            Played = true;
        }
    }
}
