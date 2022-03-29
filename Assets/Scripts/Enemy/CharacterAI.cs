using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.AI;

[BurstCompile]
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
    [SerializeField, ReadOnly] private Vector3 vel;
    private float destinationThreshold = 1f;
    private bool doOnce = false;
    private bool stopAI = false;
    public bool atDestination = true;

    [SerializeField, Space]
    private float killRad = 1f;

    [Header("Animation Settings"), Space]
    [ReadOnly] public string isMoving = "IsMoving";

    [ReadOnly] public string roar1 = "Roar1";
    [ReadOnly] public string roar2 = "Roar2";
    [SerializeField] private bool setRoar;
    private Coroutine roarHandler = null;

    [SerializeField, Space] private bool surveying;
    public int surveyTimes = 0;

    [Header("Detection Settings"), Space]
    public bool heard;

    [SerializeField]
    private SphereCollider sphereCollider;

    private Vector3 samplePosition;

    [Range(0f, 100f), Space]
    public float heardRange;

    [Space]
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
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(samplePosition, .5f);

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

    #region Public Functions
    public Vector3 GetWaypointPosition(int id)
    {
        Waypoint waypoint = waypoints[id];

        Vector3 pos = waypoint.position;
        if (waypoint.radius == 0) return pos;
        Vector3 rad = Random.insideUnitSphere * waypoint.radius;
        rad.y = 0;
        samplePosition = pos + rad;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(pos + rad, out hit, 1f, NavMesh.AllAreas))
        {
            Vector3 finalPos = hit.position;
            finalPos.y = 0;
            Debug.Log("[GetWaypointPosition] Destination: " + finalPos);
            return finalPos;
        }
        Vector3 errorPos = pos + rad;
        Debug.LogWarning("[AreaToSurvay] Destination: " + errorPos);
        return GetWaypointPosition(id);
    }

    public Vector3 GetTargetPosition(Vector3 targetPos)
    {
        Vector3 pos = targetPos;
        Vector3 rad = Random.Range(5f, 20f) * Random.insideUnitSphere;
        rad.y = 0;
        samplePosition = pos + rad;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(pos + rad, out hit, 2f, NavMesh.AllAreas))
        {
            Vector3 finalPos = hit.position;
            finalPos.y = 0;
            Debug.Log("[GetTargetPosition] Destination: " + finalPos);
            return finalPos;
        }
        else if (NavMesh.SamplePosition(pos + rad, out hit, 5f, NavMesh.AllAreas))
        {
            Vector3 finalPos = hit.position;
            finalPos.y = 0;
            Debug.Log("[AreaToSurvay] Destination: " + finalPos);
            return finalPos;
        }
        else
        {
            Vector3 errorPos = pos + rad;
            Debug.LogWarning("[AreaToSurvay] Destination: " + errorPos);
            return pos + rad;
        }
    }

    private Vector3 AreaToSurvey(Vector3 position)
    {
        Vector3 pos = position;
        Vector3 rad = Random.Range(3f, 8f) * Random.insideUnitSphere;
        rad.y = 0;
        samplePosition = pos + rad;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(pos + rad, out hit, 1.5f, NavMesh.AllAreas))
        {
            Vector3 finalPos = hit.position;
            finalPos.y = 0;
            Debug.Log("[AreaToSurvay] Destination: " + finalPos);
            return finalPos;
        }
        else if (NavMesh.SamplePosition(pos + rad, out hit, 5f, NavMesh.AllAreas))
        {
            Vector3 finalPos = hit.position;
            finalPos.y = 0;
            Debug.Log("[AreaToSurvay] Destination: " + finalPos);
            return finalPos;
        }
        else
        {
            Vector3 errorPos = pos + rad;
            Debug.LogWarning("[AreaToSurvay] Destination: " + errorPos);
            return AreaToSurvey(position);
        }
    }

    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //Task Set Up
        moveTarget = GetComponent<MoveToTarget>();
        moveWaypoint = GetComponent<ToNextWaypoint>();

        atDestination = true;
        destination = gameObject.transform.position;

        sphereCollider.radius = heardRange;

        Destination = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        vel = navMeshAgent.velocity;
        AnimationUpdate();

        if (stopAI)
            navMeshAgent.isStopped = true;
        else if (!stopAI)
            navMeshAgent.isStopped = false;

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

    private void LateUpdate()
    {
        Vector2 distance = new Vector2(gameObject.transform.position.x - Destination.x, gameObject.transform.position.z - Destination.z);
        AtDestination(distance);
    }

    private void AtDestination(Vector2 distance)
    {
        if (distance.magnitude > destinationThreshold)
            atDestination = false;
        else
            atDestination = true;

        if (!heard)
        {
            if (atDestination)
                if (!surveying)
                    StartCoroutine(SurveyArea(Destination));
            if (!atDestination)
            {
                navMeshAgent.SetDestination(Destination);
            }
        }
    }

    private void WaypointCheck()
    {
        if (waypoint_bool && !heard)
        {
            moveWaypoint.MovetoWaypoint();
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

    private IEnumerator SurveyArea(Vector3 position)
    {
        surveying = true;
        surveyTimes = 0;
        List<Vector3> finalPos = new List<Vector3>();
        for (int i = 0; i < 3; i++)
        {
            Vector3 targetpos = AreaToSurvey(position);
            finalPos.Add(targetpos);
        }
        for (int i = 0; i < 3; ++i)
        {
            if (atDestination)
            {
                animator.SetTrigger("Search");
                stopAI = true;
                yield return new WaitForSeconds(4.333f);
                Debug.Log("[Survey Area] NavMesh Agent is Stopped");
                stopAI = false;
                Destination = finalPos[i];
                surveyTimes++;
                yield return new WaitForSeconds(1f);
            }
        }
        if (heard)
        {
            moveTarget.ToDestination(target, navMeshAgent);
            heard = false;
        }
        else if (waypoint_bool)
        {
            WaypointCheck();
        }
        finalPos.Clear();
        surveying = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("PLAYER");
            target = other.gameObject;
            heard = true;
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
        if(navMeshAgent.velocity.magnitude > 0.1f)
        {
            animator.SetFloat("Speed", .5f);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
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