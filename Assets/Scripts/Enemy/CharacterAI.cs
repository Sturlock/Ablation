using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAI : MonoBehaviour
{
    private Vector3 currentPos;

    [SerializeField]
    private Animator animator;

    private NavMeshAgent navMeshAgent;
    private GameObject target;
    private ToNextWaypoint moveWaypoint;
    private MoveToTarget moveTarget;
    [SerializeField, ReadOnly] private Vector3 destination;
    private float destinationThreshold = 1f;
    private bool doOnce = false;
    public bool atDestination = true;

    [SerializeField, Space]
    private float killRad = 1f;

    [Header("Animation Settings"), Space]
    [ReadOnly] public string isMoving = "IsMoving";

    [ReadOnly] public string roar1 = "Roar1";
    [ReadOnly] public string roar2 = "Roar2";
    [SerializeField] private bool setRoar;
    private Coroutine roarHandler = null;

    [Header("Detection Settings"), Space]
    public bool heard;

    private SphereCollider sphereCollider;

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

    public Vector3 LastKnownPosition
    {
        get => lastKnownPos;
        set => lastKnownPos = value;
    }

    #endregion Getters and Setters

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

        Destination = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        AnimationUpdate();
        if (heard && !wasKnown)
        {
            moveTarget.ToDestination(target, navMeshAgent);
            heard = false;
            return;
        }
        if (waypoint_bool)
            WaypointCheck();

        if (heard && atDestination)
        {
            SurvayArea(destination);
            heard = false;
        }
        Vector2 distance = new Vector2(gameObject.transform.position.x - Destination.x, gameObject.transform.position.z - Destination.z);
        AtDestination(distance);

        #region DEBUG

#if UNITY_EDITOR
        if (sphereCollider.radius != heardRange)
        {
            sphereCollider.radius = heardRange;
        }
#endif

        #endregion DEBUG
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
        }
        if (!heard)
        {
            if (!atDestination)
            {
                navMeshAgent.SetDestination(Destination);
            }
        }
        else
        {
            if (!atDestination)
            {
                SurvayArea(Destination);
            }
        }
    }

    private void WaypointCheck()
    {
        if (waypoint_bool && !heard)
        {
            if (atDestination && !doOnce)
            {
                moveWaypoint.MovetoWaypoint();
            }
        }
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

    private IEnumerator SurvayArea(Vector3 poition)
    {
        Vector3 targetPos = poition;
        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = poition;
            Vector3 rad = Random.Range(2f, 5f) * Random.insideUnitSphere;
            rad.y = 0;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(pos + rad, out hit, .1f, NavMesh.AllAreas))
            {
                Debug.Log("Destination: True");
                targetPos = hit.position;
            }
            targetPos = pos + rad;
            yield return new WaitForSeconds(1f);
        }
        Destination = targetPos;
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
        }
        if (other.tag == "Door")
        {
            Debug.Log("DOOR");
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

    private void AnimationUpdate()
    {
        animator.SetBool(isMoving, navMeshAgent.velocity.magnitude > 0.01f);
        string roar;
        float seconds;
        float i;

        if (setRoar)
        {
            i = Random.Range(0, 1);
            switch (i)
            {
                case 0:
                    roar = roar1;
                    seconds = 2.567f;
                    break;

                case 1:
                    roar = roar2;
                    seconds = 2.033f;
                    break;

                default:
                    roar = roar1;
                    seconds = 2.567f;
                    break;
            }
            roarHandler = StartCoroutine(PlayRoar(roar, seconds));
            setRoar = false;
        }

        if (roarHandler == null)
            navMeshAgent.isStopped = false;
    }

    private IEnumerator PlayRoar(string roar, float seconds)
    {
        animator.SetTrigger(roar);
        navMeshAgent.isStopped = true;

        yield return new WaitForSeconds(seconds);
        roarHandler = null;
    }
}