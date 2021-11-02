using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : MonoBehaviour
{
    
    CharacterAI characterAI;
    
    public void ToDestination(Transform target)
    {
        characterAI.Destination = target.position;
    }
}


