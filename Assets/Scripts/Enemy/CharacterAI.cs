using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAI : MonoBehaviour
{
    private Vector3 currentPos;

    [SerializeField]
    private float killRad = 1f;

    private NavMeshAgent navMeshAgent;
    private GameObject target;
    private ToNextWaypoint moveWaypoint;
    private MoveToTarget moveTarget;
    [SerializeField] private Vector3 destination;
    private float destinationThreshold = 1f;
    private bool doOnce = false;
    public bool atDestination = true;


    
    private SphereCollider sphereCollider;
    [Header("Detection Settings"), Space]
    public bool heard;
    [Range(0f, 100f)] 
    public float heardRange;
    public Vector3 lastKnownPos;
    public bool wasKnown;
    
    [Header("Waypoints"), Space]
    [SerializeField] private bool waypoint_bool;

    [SerializeField] private float maxPathLenght = 50f;
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

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, killRad);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gameObject.transform.position, heardRange);

        Gizmos.color = Color.magenta;
        if (navMeshAgent != null && navMeshAgent.path != null && navMeshAgent.path.corners != null)
        {
            for (int i = 0; i < navMeshAgent.path.corners.Length - 1; i++)
            {
                Gizmos.DrawLine(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
            }
        }


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
    public bool WasKnown
    {
        get => wasKnown;
        set => wasKnown = value;
    }

    public float MaxPathLenght
    {
        get => maxPathLenght;
        set => maxPathLenght = value;
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
        return GetWaypointPosition(id);
    }

    public Vector3 GetTargetPosition(Vector3 targetPos)
    {
        Vector3 pos = targetPos;
        Vector3 rad = Random.Range(5f, 20f) * Random.insideUnitSphere;
        rad.y = 0;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(pos + rad, out hit, .1f, NavMesh.AllAreas))
        {
            Debug.Log("Destination: True");
            return hit.position;
        }
        return pos + rad;
    }

    public Vector3 LastKnownPosition
    {
        get => lastKnownPos;
        set => lastKnownPos = value;
    }

    #endregion Getters and Setters

    // Start is called before the first frame update
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //Task Set Up
        moveTarget = GetComponent<MoveToTarget>();
        moveWaypoint = GetComponent<ToNextWaypoint>();
        
        atDestination = true;
        destination = gameObject.transform.position;

        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = heardRange;
    }

    // Update is called once per frame
    private void Update()
    {
        if (heard && !wasKnown)
        {
            moveTarget.ToDestination(target, navMeshAgent);
            heard = false;
            return;
        }
        if (waypoint_bool && !heard)
        {
            if (atDestination && !doOnce)
            {
                WaypointCheck();
            }
        }
        if (heard && atDestination)
        {
            heard = false;
        }
        Vector2 distance = new Vector2(gameObject.transform.position.x - destination.x, gameObject.transform.position.z - destination.z);
        AtDestination(distance);

        #region DEBUG
        #if UNITY_EDITOR
        if(sphereCollider.radius != heardRange)
        {
            sphereCollider.radius = heardRange;
        }
        #endif
        #endregion
    }

    private void FixedUpdate()
    {
        currentPos = transform.position;
    }

    private void AtDestination(Vector2 distance)
    {
        if (distance.magnitude > destinationThreshold)
            atDestination = false;
        else
        {
            atDestination = true;
            Kill();
        }
        if (!atDestination)
        {
            navMeshAgent.SetDestination(destination);
        }
    }

    private void WaypointCheck()
    {
        moveWaypoint.MovetoWaypoint();
    }

    private void Kill()
    {
        RaycastHit hit;
        if (target != null)
        {
            Vector3 direction = transform.position.Direction(target.transform.position);
            if (Physics.Raycast(currentPos, direction, out hit, killRad))
            {
                Debug.DrawRay(currentPos, direction, Color.cyan);
                GameObject hitObj = hit.collider.gameObject;
                if (hitObj.CompareTag("Player"))
                {
                    Debug.Log("Kill Player");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject.GetComponent<SphereCollider>().enabled == true)
            {
                Debug.Log("PLAYER");
                target = other.gameObject;
                heard = true;
            }
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

    private void OnTriggerStay(Collider other)
    {
        //if (other.tag == "Player")
        //{
        //    Debug.Log("PLAYER");
        //    target = other.gameObject;
        //    heard = true;
        //}
    }
}