using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDetection : MonoBehaviour
{
    CharacterAI characterAI;
    GameObject target;
    bool heard;

    private void Awake()
    {
        characterAI = GetComponentInParent<CharacterAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("PLAYER");
            target = other.gameObject;
            heard = true;
            characterAI.IsHeard(target, heard);
        }
        if (other.tag == "SOUND")
        {
            target = other.gameObject;
            heard = true;
        }
    }
}
