using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour, IInteractable
{
    public ClearenceLvl lvlRequired = ClearenceLvl.blue;
    public SecurityClearance clearance;
    public TestDialogue dialogue;
    public Animator animator;
    public AudioClip ElevatorOpen;
    public AudioClip ElevatorClose;
    [SerializeField] private AudioSource audioSource;
    public void Action(PlayerInteract script)
    {
        throw new System.NotImplementedException();
    }

    public void Interact(PlayerInteract script)
    {
        if(clearance.lvl == lvlRequired)
        {
            audioSource.PlayOneShot(ElevatorOpen);
            animator.SetBool("Open", true);
            //StartCoroutine(CloseElevator());
        }
        else if(clearance.lvl != lvlRequired)
        {
            dialogue.OnClick();
        }
    }

    //public IEnumerator CloseElevator()
    //{
    //    yield return new WaitForSeconds(11f);
    //    animator.SetBool("Open", false);
    //}

    public void CloseElevator()
    {
        audioSource.PlayOneShot(ElevatorClose);
        animator.SetBool("Open", false);
    }

    // Start is called before the first frame update
}
