using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateDialogue : MonoBehaviour, IInteractable
{
    public AudioClip dialogueClip;
    public Animator ani;
    public bool played = false;
    public bool Inside = false;

    public void Action(PlayerInteract script)
    {
        throw new System.NotImplementedException();
    }

    public void Interact(PlayerInteract script)
    {
        played = false;
        ani.SetBool("Help", true);
        FindObjectOfType<ShowHideHandy>().can = true;
        gameObject.SetActive(false);
        DialogueManager.Instance.BeginDialogue(dialogueClip);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !played)
        {
            ani.SetBool("Activate", true);
            
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ani.SetBool("Activate", false);
            

        }
    }
}
