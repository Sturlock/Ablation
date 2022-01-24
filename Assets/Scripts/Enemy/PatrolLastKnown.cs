using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolLastKnown : MonoBehaviour
{
    NavMeshPath path;
    Vector3 targetPos;
    int corners;
    public void PatrolHeardLocation(GameObject target, NavMeshAgent agent)
    {
        path = new NavMeshPath();
        targetPos = target.transform.position;
        //First Point is Where it thought you where

        //Second Point in the general direction of where you went
        
        //Third Point Check point far from player 
        
        //Leave
    }
}
