using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    Transform followTarget;
    MoveToTarget move;
    [SerializeField] Vector3 destination;

    public bool atDestination;

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
        move = GetComponent<MoveToTarget>();
        move.WaypointStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoint_bool)
        {
            if (atDestination)
            {
                move.MovetoWaypoint();
            }
        }
        if ((destination.x != gameObject.transform.position.x) && (destination.z != gameObject.transform.position.z))
            atDestination = false;
        else atDestination = true;

        if (!atDestination)
            navMeshAgent.SetDestination(destination);

        
        

    }
}
