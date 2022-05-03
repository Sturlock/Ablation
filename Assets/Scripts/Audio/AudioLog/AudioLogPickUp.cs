using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLogPickUp : MonoBehaviour, IInteractable
{
    public AudioSource audioSource;
    //public AudioClip Pickup;
    public AudioClip AudioLog;
    public GameObject Recorder;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Recorder.transform.position, 1.5f);
    }
    public void Action(PlayerInteract script)
    {
        throw new System.NotImplementedException();
    }

    public void Interact(PlayerInteract script)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = AudioLog;
        audioSource.PlayOneShot(AudioLog);
        //DialogueManager.Instance.BeginDialogue(AudioLog);
        Recorder.SetActive(false);

    }
}
