using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWalk : MonoBehaviour
{
    public WalkingAway _walkingAway;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        StartCoroutine(_walkingAway.MovePlace());
    }
}
