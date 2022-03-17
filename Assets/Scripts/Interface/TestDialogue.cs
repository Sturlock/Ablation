using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    public AudioClip dialogueClip;

    public void OnClick()
    {
        DialogueManager.Instance.BeginDialogue(dialogueClip);
    }
}
