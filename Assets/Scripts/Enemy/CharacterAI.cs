using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject target;
    private ToNextWaypoint moveWaypoint;
    private MoveToTarget moveTarget;
    [SerializeField] private Vector3 destination;
    private float destinationThreshold = 1f;
    private bool doOnce = false;
    public bool atDestination = true;
    public bool heard;

    [Header("Waypoints"), Space]
    [SerializeField] private bool waypoint_bool;

    [SerializeField] private bool loop;
    [SerializeField] private bool randomWaypoint;
    [SerializeField] private List<Waypoint> waypoints = new List<Waypoint>();
    private int maxWaypoints;
    private int currentWaypoint;

    #region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, destination);
    }

    #endregion Gizmos

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
            return pos + rad;
        }
        return hit.position;
    }

    public Vector3 GetTargetPosition(Vector3 targetPos)
    {
        Vector3 pos = targetPos;
        Vector3 rad = Random.Range(1f, 12f) * Random.insideUnitSphere;
        rad.y = 0;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(pos + rad, out hit, .1f, NavMesh.AllAreas))
        {
            Debug.Log("Destination: True");
            return hit.position;
        }
        return pos + rad;
    }

    #endregion Getters and Setters

    // Start is called before the first frame update
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        moveTarget = GetComponent<MoveToTarget>();
        moveWaypoint = GetComponent<ToNextWaypoint>();
        atDestination = true;
        destination = gameObject.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (heard)
        {
            moveTarget.ToDestination(target);
            heard = false;
            return;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("PLAYER");
//             target = other.gameObject;
//             heard = true;
        }
        if (other.tag == "SOUND")
        {
            Debug.Log("SOUND");
//             target = other.gameObject;
//             heard = true;
        }
        if (other.tag == "Door")
        {
            Debug.Log("DOOR");
            //target = other.gameObject;
            //heard = true;
        }
        //else Debug.Log("Not Sound");
    }
}