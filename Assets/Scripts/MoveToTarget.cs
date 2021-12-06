using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : MonoBehaviour
{
    private CharacterAI characterAI;
    [SerializeField] private NavMeshPath navMeshPath = new();

    private void Start()
    {
        characterAI = GetComponent<CharacterAI>();
    }

    public void ToDestination(GameObject target)
    {
        Vector3 targetpos = target.transform.position;
        RaycastHit hit;
        NavMesh.CalculatePath(characterAI.transform.position, targetpos, NavMesh.AllAreas, navMeshPath);

        if (navMeshPath.corners.Length > 40f)
        {
            if (Physics.Raycast(characterAI.transform.position, -targetpos, out hit, 20f))
            {
                Debug.DrawRay(characterAI.transform.position, targetpos);
                if (target.CompareTag(hit.collider.tag))
                {
                    characterAI.Destination = targetpos;
                    return;
                }
                if (!target.CompareTag(hit.collider.tag))
                {
                    targetpos = characterAI.GetTargetPosition(targetpos);
                    characterAI.Destination = targetpos;
                    return;
                }
            }
        }
        navMeshPath.ClearCorners();
        return;
    }
}