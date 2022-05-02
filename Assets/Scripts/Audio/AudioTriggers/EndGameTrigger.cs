using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public Animator animator;

    public void EndGameFade()
    {
        animator.SetBool("EndGame", true);
    }
}
