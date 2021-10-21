using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Transform target;
    [Header("Waypoints"), Space]
    public bool waypoint_bool;
    [SerializeField] List<Waypoint> waypoints = new List<Waypoint>();



    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (waypoint_bool)
        {
            if (waypoints == null) return;
            else
            {
                
            }
        }
        else
        {
            if (target == null) return;
            else
            {
                navMeshAgent.SetDestination(target.position);

            }
        }
        
    }
}
