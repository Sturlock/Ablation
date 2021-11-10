using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : MonoBehaviour
{
    
    CharacterAI characterAI;
    void Start()
    {
        characterAI = GetComponent<CharacterAI>();
    }

    public void ToDestination(Vector3 target)
    {
        characterAI.Destination = target;
    }
}


