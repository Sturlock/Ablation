using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public Animator animator;
    bool doOnce = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !doOnce)
        {
            StartCoroutine(End());
            doOnce = true;
        }
    }

    public IEnumerator End()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("Activate", true);
        yield return new WaitForSeconds(21f);
        GameManager.Instance.LoadLevel("MainMenu");
        GameManager.Instance.UnloadLevel("Level_Asset");
    }
}
