
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioS;
    public AudioMixerSnapshot idleSnapshot;
    public AudioMixerSnapshot auxInSnapshot;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jawbreaker"))
        {
            auxInSnapshot.TransitionTo(0.5f);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Jawbreaker"))
        {
            idleSnapshot.TransitionTo(0.5f);
        }
    }
}
