using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    GameObject target;
    ToNextWaypoint moveWaypoint;
    MoveToTarget moveTarget;
    [SerializeField] Vector3 destination;
    float destinationThreshold = 0.1f;
    bool doOnce = false;
    public bool atDestination = true;
    public bool heard;

    [Header("Waypoints"), Space]
    [SerializeField] bool waypoint_bool;
    [SerializeField] bool loop;
    [SerializeField] bool randomWaypoint;
    [SerializeField] List<Waypoint> waypoints = new List<Waypoint>();
    int maxWaypoints;
    int currentWaypoint;
    

    #region Getters and Setters
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
        NavMeshHit hit;
        if (NavMesh.SamplePosition(pos + rad, out hit, 1f, NavMesh.AllAreas))
        {
            Debug.Log("Destination: True");
            return hit.position;
        }
        return pos + rad;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        moveTarget = GetComponent<MoveToTarget>();  
        moveWaypoint = GetComponent<ToNextWaypoint>();
        atDestination = true;
        //moveWaypoint.WaypointStart();
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
            if (atDestination & !doOnce)
            {
                WaypointCheck();
            } 
        }
        Vector2 distance = new Vector2(gameObject.transform.position.x - destination.x, gameObject.transform.position.z - destination.z);
        //Debug.Log(distance.magnitude);
        if (distance.magnitude > destinationThreshold)
            atDestination = false;
        else atDestination = true;
        if (!atDestination)
            navMeshAgent.SetDestination(destination);

        if (heard && atDestination) 
        { 
            heard = false; 
        }
    }

    private void WaypointCheck()
    {
        moveWaypoint.MovetoWaypoint();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject;
            heard = true;
        }
        if (other.tag == "SOUND")
        {
            target = other.gameObject;
            heard = true;
        }
        if (other.tag == "Door")
        {
            target = other.gameObject;
            heard = true;
        }
        //else Debug.Log("Not Sound");
    }
}
