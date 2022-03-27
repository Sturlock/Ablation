using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlay : MonoBehaviour
{
    public AudioClip dialogueClip;

    public void PlayClip()
    {
        DialogueManager.Instance.BeginDialogue(dialogueClip);
    }
}
