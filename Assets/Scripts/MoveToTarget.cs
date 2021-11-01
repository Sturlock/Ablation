using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : MonoBehaviour
{
    
    [SerializeField] Transform target;
    CharacterAI characterAI;

    public int currentWaypoint;

    // Start is called before the first frame update
    void OnEnable()
    {
        characterAI = GetComponent<CharacterAI>();  
    }


    // Update is called once per frame
    public void MovetoWaypoint()
    {
        if (characterAI.Waypoints.Count == 0) return;

        if (characterAI.UsingWaypoint)
        {
            if (characterAI.Waypoints == null) return;

            if (characterAI.SelectRandomWaypoint)
            {
                currentWaypoint = Random.Range(0, characterAI.Waypoints.Count);
                characterAI.Destination = characterAI.GetWaypointPosition(currentWaypoint);
            }
            if (target == null) return;
            else
            {
                characterAI.NavMeshAgent.SetDestination(target.position);

            }
        }
       
        
    }
}


