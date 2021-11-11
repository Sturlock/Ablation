using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    Vector3 target;
    ToNextWaypoint moveWaypoint;
    MoveToTarget moveTarget;
    [SerializeField] Vector3 destination;

    public bool atDestination = true;
    public bool heard;

    [Header("Waypoints"), Space]
    [SerializeField] bool waypoint_bool;
    [SerializeField] bool loop;
    [SerializeField] bool randomWaypoint;
    [SerializeField] List<Waypoint> waypoints = new List<Waypoint>();
    int maxWaypoints;
    int currentWaypoint;

    public NavMeshAgent NavMeshAgent
    {
        get => navMeshAgent;
        set => navMeshAgent = value;
    }
    public Vector3 Destination
    {
        get => destination;
        set => destination = value;
    }
    public List<Waypoint> Waypoints
    {
        get => waypoints;
        set => waypoints = value;
    }
    public int MaxWaypoints
    {
        get => maxWaypoints;
        set => maxWaypoints = waypoints.Count;
    }
    public int CurrentWaypoint
    {
        get => currentWaypoint;
        set => currentWaypoint = value;
    }
    public bool UsingWaypoint
    {
        get => waypoint_bool;
        set => waypoint_bool = value;
    }
    public bool SelectRandomWaypoint 
    {
        get => randomWaypoint;
        set => randomWaypoint = value;
    }
    public bool LoopWaypoint
    {
        get => loop;
        set => loop = value;
    }
    public Vector3 GetWaypointPosition(int id)
    {
        return waypoints[id].position;
    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        moveTarget = GetComponent<MoveToTarget>();
        moveWaypoint = GetComponent<ToNextWaypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (heard)
        {
            moveTarget.ToDestination(target);
            
        }
        if (waypoint_bool && !heard)
        {
            if (atDestination)
            {
                WaypointCheck();
            }
            
        }
        if ((gameObject.transform.position.x != destination.x) && (gameObject.transform.position.z != destination.z))
            atDestination = false;
        else atDestination = true;
        if (!atDestination)
            navMeshAgent.SetDestination(destination);

        if (heard) 
        { 
            heard = false; 
        }
    }

    private void WaypointCheck()
    {
        moveWaypoint.MovetoWaypoint();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "SOUND")
        {
            target = other.transform.position;
            heard = true;
        }
        else Debug.Log("Not Sound");
    }
}
