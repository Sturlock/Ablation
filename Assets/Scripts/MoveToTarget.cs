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

    public void ToDestination(GameObject target)
    {
        Vector3 targetpos = target.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(characterAI.transform.position, targetpos, out hit, 20f))
        {
            if (target.CompareTag(hit.collider.tag))
            {
                characterAI.Destination = targetpos;
            }
        }
    }
}