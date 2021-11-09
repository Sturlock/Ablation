
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum CState
{
    Idle,
    Detected,


}
public class CharacterAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    Transform target;
    ToNextWaypoint moveWaypoint;
    MoveToTarget moveTarget;
    [SerializeField] Vector3 destination;

    public bool atDestination;
    bool heard;
    CState state;

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
        Waypoint waypoint = waypoints[id];

        Vector3 pos = waypoint.position;
        if (waypoint.radius == 0) return pos;
        Vector3 rad = Random.insideUnitSphere * waypoint.radius;
        rad.y = 0;
        return pos + rad;
    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        moveTarget = GetComponent<MoveToTarget>();
        moveWaypoint = GetComponent<ToNextWaypoint>();
        moveWaypoint.WaypointStart();
    }

    // Update is called once per frame
    void Update()
    {
        if(heard)
        {
            moveTarget.ToDestination(target.transform);
        }
        if (waypoint_bool)
        {
            WaypointCheck();
            
        }
    }

    private void WaypointCheck()
    {
        switch (state)
        {
        case (CState.Idle):
                if (atDestination)
                {
                    moveWaypoint.MovetoWaypoint();
                }
                break;
        case (CState.Detected):

                break;
            default:
                break;
        }
            
        
        if ((destination.x != gameObject.transform.position.x) && (destination.z != gameObject.transform.position.z))
            atDestination = false;
        else atDestination = true;

        if (!atDestination)
            navMeshAgent.SetDestination(destination);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SOUND")
        {
            target = other.transform;
            heard = true;
            

        }
    }
}
