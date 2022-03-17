using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AI_Director : Singleton<AI_Director>
{
    [Range(0f, 100f), SerializeField]
    private float tension;
    private bool protectedArea;
    private Coroutine tensionHandle;
    private NavMeshPath AIPath;
    private float distanceFromPlayer;


    [Space, SerializeField, Header("Characters")]
    private GameObject AI;
    [SerializeField]
    private GameObject Player;

    private CharacterAI characterAI;

    [Space,SerializeField]
    private float stationDownTimer;
    [SerializeField]
    private float requiredHoldTime;

    private void Start()
    {
        characterAI = GetComponent<CharacterAI>();
        AIPath = new NavMeshPath();
    }
    private void Update()
    {
        FindPlayer();

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
                WaveOff();
                stationDownTimer = 0;
            }
        }
        else
        {
            stationDownTimer = 0;
        }
    }

    private void FindPlayer()
    {
        characterAI.NavMeshAgent.CalculatePath(Player.transform.position, AIPath);
        distanceFromPlayer = AIPath.Length();
    }

    public IEnumerator IncreaseTension()
    {
        while (tension < 100f)
        {
            tension++;
            yield return new WaitForSeconds(1f);
        }
        tensionHandle = null;
    }
    public IEnumerator ReduceTension()
    {
       while(tension > 20f)
        {
            tension--;
            yield return new WaitForSeconds(1f);
        }
        tensionHandle = null;
    }

    private void WaveOff()
    {
        Interupt();
        tensionHandle = StartCoroutine(ReduceTension());

    }
    public void Interupt()
    {
        if (tensionHandle != null)
        {
            StopCoroutine(tensionHandle);
            tensionHandle = null;
        }
    }
    private Vector3 HintPlayerLocation(Vector3 position, bool procArea)
    {
        Vector3 pos = position;
        Vector3 rad = Random.Range(10f, 30f) * Random.insideUnitSphere;
        rad.y = 0;
        NavMeshHit hit;
        if (!procArea)
        {
            if (NavMesh.SamplePosition(pos + rad, out hit, .1f, NavMesh.AllAreas))
            {
                Debug.Log("Destination: True");
                return hit.position;
            }
            return pos + rad; 
        }
        else 
        {
            CharacterAI characterAI = AI.GetComponent<CharacterAI>();
            return characterAI.Destination;
        }
        
    }
}
