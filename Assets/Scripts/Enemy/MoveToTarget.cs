using UnityEngine;
using UnityEngine.AI;

[HideInInspector]
public class MoveToTarget : MonoBehaviour
{
    private CharacterAI characterAI;
    [SerializeField] private Vector3 targetPos = Vector3.zero;
    private NavMeshPath path = null;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (Application.isPlaying)
        {
            Gizmos.DrawLine(characterAI.transform.position, targetPos);
            Gizmos.DrawWireSphere(characterAI.transform.position, 30f);
        }
    }

    private void Start()
    {
        characterAI = GetComponent<CharacterAI>();
        path = new NavMeshPath();
    }

    public void ToDestination(GameObject target, NavMeshAgent agent)
    {
        targetPos = target.transform.position;
        agent.CalculatePath(targetPos, path);
        float pathLenth = path.Length();
        Vector3 targetDirection = characterAI.transform.position.Direction(targetPos);
            RaycastHit hit;
            if (Physics.Raycast(characterAI.transform.position, targetDirection, out hit))
            {
                if (pathLenth < characterAI.MaxPathLenght)
                {
                    Debug.DrawRay(characterAI.transform.position, targetDirection, Color.blue);
                    if (target.CompareTag(hit.collider.tag))
                    {
                        characterAI.Destination = targetPos;
                        characterAI.lastKnownPos = targetPos;
                        characterAI.WasKnown = true;
                    }
                    else if (!target.CompareTag(hit.collider.tag))
                    {
                        targetPos = characterAI.GetTargetPosition(targetPos);
                        characterAI.Destination = targetPos;
                        characterAI.lastKnownPos = targetPos;
                        characterAI.WasKnown = true;
                    }
                }
            }
    }
}