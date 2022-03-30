using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIDirector : Singleton<AIDirector>
{
    [Range(0f, 100f)]
    public float tension;

    public bool protectedArea;
    private Coroutine tensionHandle;
    private NavMeshPath AIPath;
    private float distanceFromPlayer;

    [Space, Header("Characters")]
    public GameObject AI;

    public GameObject Player;

    public CharacterAI characterAI;

    [Space, SerializeField]
    private float stationDownTimer;

    [SerializeField]
    private float requiredHoldTime;

    private void Start()
    {
        characterAI = AI.GetComponent<CharacterAI>();
        AIPath = new NavMeshPath();
    }

    private void Update()
    {
        //FindPlayer();

        tension = Mathf.Clamp(tension, 0f, 100f);
        if (tension < 20)
        {
            HintPlayerLocation(Player.transform.position, protectedArea);
        }
        if (tension == 100f)
        {
            stationDownTimer += Time.deltaTime;
            if (stationDownTimer >= requiredHoldTime)
            {
                //WaveOff();
                stationDownTimer = 0;
            }
        }
        else
        {
            stationDownTimer = 0;
        }
    }

    public void FindPlayer()
    {
        characterAI.NavMeshAgent.CalculatePath(Player.transform.position, AIPath);
        distanceFromPlayer = AIPath.Length();
    }

    public IEnumerator IncreaseTension(float inc)
    {
        while (tension < 100f)
        {
            tension += inc;
            yield return new WaitForSeconds(1f);
        }
        tensionHandle = null;
    }

    public IEnumerator ReduceTension(float dec)
    {
        while (tension > 20f)
        {
            tension -= dec;
            yield return new WaitForSeconds(1f);
        }
        tensionHandle = null;
    }

    public void WaveOff(int i)
    {
        Interupt();
        tensionHandle = StartCoroutine(ReduceTension(i));
    }

    public void Interupt()
    {
        if (tensionHandle != null)
        {
            StopCoroutine(tensionHandle);
            tensionHandle = null;
        }
    }

    public Vector3 HintPlayerLocation(Vector3 position, bool procArea)
    {
        Vector3 pos = position;
        Vector3 rad = Random.Range(10f, 30f) * Random.insideUnitSphere;
        rad.y = 0;
        NavMeshHit hit;
        if (!procArea)
        {
            if (NavMesh.SamplePosition(pos + rad, out hit, 1f, NavMesh.AllAreas))
            {
                Debug.Log("Destination: True");
                return hit.position;
            }
            return pos + rad;
        }
        else
        {
            return characterAI.Destination;
        }
    }

    public void GiveDestination(Vector3 position)
    {
        Debug.Log(position.ToString());
        characterAI.Destination = position;
    }

    public bool IsPlayerNearAlienT1()
    {
        if (AIPath.Length() <= 20)
        {
            return true;
        }
        return false;
    }

    public bool IsPlayerNearAlienT2()
    {
        if (AIPath.Length() <= 10)
        {
            return true;
        }
        return false;
    }
}