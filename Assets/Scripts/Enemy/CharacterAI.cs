using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.AI;

[BurstCompile]
public class CharacterAI : Singleton<CharacterAI>
{
    private Vector3 currentPos;

    [SerializeField]
    private Animator _animator;
    public Animator transition;

    [SerializeField]
    private AudioSource _audioSource;

    public AudioClip Search;
    public AudioClip Roar;
    public AudioClip HaroldDeath;

    private NavMeshAgent _navMeshAgent;
    private GameObject _target;
    private ToNextWaypoint moveWaypoint;
    private MoveToTarget moveTarget;
    [SerializeField, ReadOnly] private Vector3 destination;
    [SerializeField, ReadOnly] private Vector3 vel;
    private float destinationThreshold = 1f;
    private bool doOnce = false;
    private bool stopAI = false;
    public bool atDestination = true;

    [SerializeField, Space]
    public float killRad = 1.5f;
    private bool killed = false;

    [Header("Animation Settings"), Space]
    [ReadOnly] public string isMoving = "IsMoving";
    [ReadOnly] public string roar1 = "Roar1";
    [ReadOnly] public string roar2 = "Roar2";

    [SerializeField] private bool _setRoar;
    private Coroutine _roarHandler = null;

    [Header("Behavioral Settings"), Space]
    [SerializeField] private bool _surveying;

    public int _surveyTimes = 0;
    private Coroutine _survayHandeler = null;

    [Header("Detection Settings"), Space]
    public bool _heard;

    [SerializeField]
    private SphereCollider sphereCollider;

    private Vector3 samplePosition;
    private Vector3 onNMPosition;

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
        if (_navMeshAgent != null && _navMeshAgent.path != null && _navMeshAgent.path.corners != null)
        {
            for (int i = 0; i < _navMeshAgent.path.corners.Length - 1; i++)
            {
                Gizmos.DrawLine(_navMeshAgent.path.corners[i], _navMeshAgent.path.corners[i + 1]);
            }
        }
    }

    #endregion Gizmos

    #region Getters and Setters

    public NavMeshAgent NavMeshAgent
    {
        get => _navMeshAgent;
        set => _navMeshAgent = value;
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
    public void Interupt()
    {
        if (_survayHandeler != null)
        {
            StopCoroutine(_survayHandeler);
            _survayHandeler = null;
            _surveying = false;
        }
        if (_roarHandler != null)
        {
            StopCoroutine(_roarHandler);
            _roarHandler = null;
        }
    }
    public Vector3 GetWaypointPosition(int id)
    {
        Waypoint waypoint = waypoints[id];

        Vector3 pos = waypoint.position;
        if (waypoint.radius == 0) return pos;
        Vector3 rad = Random.insideUnitSphere * waypoint.radius;
        rad.y = 0;
        samplePosition = pos + rad;

        if (OnNavMesh(samplePosition))
        {
            Debug.Log("[GetWaypointPosition] Destination: " + onNMPosition);
            return onNMPosition;
        }
        else
        {
            Vector3 errorPos = pos + rad;
            Debug.LogWarning("[GetWaypointPosition] Destination: " + errorPos);
            return GetWaypointPosition(id);
        }       
    }

    public Vector3 GetTargetPosition(Vector3 targetPos)
    {
        Vector3 pos = targetPos;
        Vector3 rad = Random.Range(5f, 20f) * Random.insideUnitSphere;
        rad.y = 0;
        samplePosition = pos + rad;
        if (OnNavMesh(samplePosition))
        {
            Debug.Log("[GetTargetPosition] Destination: " + onNMPosition);
            return onNMPosition;
        }
        else
        {
            Vector3 errorPos = pos + rad;
            Debug.LogWarning("[GetTargetPosition] Destination: " + errorPos);
            return GetTargetPosition(targetPos);
        }
    }

    private Vector3 AreaToSurvey(Vector3 position)
    {
        Vector3 pos = position;
        Vector3 rad = Random.Range(3f, 8f) * Random.insideUnitSphere;
        rad.y = 0;
        samplePosition = pos + rad;
        if (OnNavMesh(samplePosition))
        {
            Debug.Log("[AreaToSurvey] Destination: " + onNMPosition);
            return onNMPosition;
        }
        else
        {
            Vector3 errorPos = pos + rad;
            Debug.LogWarning("[AreaToSurvay] Destination: " + errorPos);
            return AreaToSurvey(position);
        }
    }

    public void IsHeard(GameObject target, bool heard)
    {
        Interupt();
        _heard = heard;
        _setRoar = true;
        moveTarget.ToDestination(target, _navMeshAgent);
        _target = target;
    }

    public void isHearing(GameObject target, bool heard)
    {
        _heard = heard;
        moveTarget.ToDestination(target, _navMeshAgent);
        _target = target;
    }

    #endregion Public Functions

    // Start is called before the first frame update
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
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
        if (!AIDirector.Instance.protectedArea)
        {
            vel = _navMeshAgent.velocity;
            AnimationUpdate();

            if (stopAI)
                _navMeshAgent.isStopped = true;
            else if (!stopAI)
                _navMeshAgent.isStopped = false;

            if (_heard)
            {
                NavMeshAgent.speed = 4;
            }
            else if (!_heard)
            {
                NavMeshAgent.speed = 1;
            }
        }
        else
        {
            Interupt();
        }

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
        if (!AIDirector.Instance.protectedArea)
        {
            Vector2 distance = new Vector2(gameObject.transform.position.x - Destination.x, gameObject.transform.position.z - Destination.z);
            AtDestination(distance);
        }
    }

    private void AtDestination(Vector2 distance)
    {
        if (distance.magnitude > destinationThreshold)
            atDestination = false;
        else
            atDestination = true;

        if (!_heard)
        {
            if (atDestination)
            {
                NavMeshAgent.velocity = Vector3.zero;
                if (!_surveying)
                    _survayHandeler = StartCoroutine(SurveyArea(Destination));
            }
            if (!atDestination)
            {
                _navMeshAgent.SetDestination(Destination);
            }
        }
        if (_heard)
        {
            if (atDestination)
            {
                NavMeshAgent.velocity = Vector3.zero;
                if (!_surveying)
                {
                    _survayHandeler = StartCoroutine(SurveyArea(LastKnownPosition));
                    _heard = false;
                }
            }
            if (!atDestination)
            {
                _navMeshAgent.SetDestination(Destination);
            }
        }
    }

    private void WaypointCheck()
    {
        if (waypoint_bool && !_heard)
        {
            moveWaypoint.MovetoWaypoint();
        }
    }

    public void Kill()
    {
        if (!killed)
        {
            Debug.Log("Kill Player");
            killed = true;


            //play Harold death sound (animation?), Pause gamescene time, fade to black, Load mainmenu
            //Place alien attacting animation trigger.
            AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

            foreach(AudioSource audioSource in audioSources)
            {
                if(audioSource.tag != "Ambient")
                {
                    audioSource.Stop();
                }
                break;
            }

            //AudioListener.pause = true;
            //_audioSource.ignoreListenerPause = true;
            _audioSource.clip = HaroldDeath;
            _audioSource.Play();
            transition.SetBool("Killed", true);
            //Time.timeScale = 0f;
            StartCoroutine(OnPK());
            
            
        }
        
    }

    private IEnumerator OnPK()
    {
        yield return new WaitForSeconds(1f);
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.LoadLevel("MainMenu");
        GameManager.Instance.UnloadLevel("GameLevel");
    }

    private IEnumerator SurveyArea(Vector3 position)
    {
        _surveying = true;
        _surveyTimes = 0;
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
                _animator.SetTrigger("Search");
                _audioSource.PlayOneShot(Search);
                stopAI = true;
                Debug.Log("[Survey Area] NavMesh Agent is Stopped");
                yield return new WaitForSeconds(4.333f);
                stopAI = false;
                Destination = finalPos[i];
                _surveyTimes= i;
                yield return new WaitForSeconds(1f);
            }
        }
        if (atDestination)
        {
            if (WasKnown)
            {
                moveTarget.ToDestination(_target, _navMeshAgent);
                WasKnown = false;
            }
            else if (waypoint_bool)
            {
                WaypointCheck();
            }
        }
        finalPos.Clear();
        _surveying = false;
        _survayHandeler = null;
    }
    private void AnimationUpdate()
    {
        float speed;
        if (_navMeshAgent.velocity.magnitude > 0.1f)
        {
            speed = _heard ? 1f : 0.5f;
            _animator.SetFloat("Speed", speed);
        }
        else
        {
            _animator.SetFloat("Speed", 0f);
        }
        string roar;
        float seconds;
        float i;
        float x;

        if (_setRoar && _roarHandler == null)
        {
            _setRoar = false;
            i = Random.Range(0, 100);
            if (i >= 80)
            {
                x = 0;
            }
            else if (i < 80 && i >= 50)
            {
                x = 1;
            }
            else { x = 2; }
            switch (x)
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
                    roar = null;
                    seconds = 0;
                    break;
            }
            if (roar != null)
            {
                _roarHandler = StartCoroutine(PlayRoar(roar, seconds));
            }
        }

        if (_roarHandler == null)
            stopAI = false;
    }

    private IEnumerator PlayRoar(string roar, float seconds)
    {
        _animator.SetTrigger(roar);
        _audioSource.PlayOneShot(Roar);
        stopAI = true;

        yield return new WaitForSeconds(seconds);

        _roarHandler = null;
    }

    private bool OnNavMesh(Vector3 targetDestination)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetDestination, out hit, 1f, NavMesh.AllAreas))
        {
            onNMPosition = hit.position;
            return true;
        }
        return false;
    }
}