using UnityEngine;
using UnityEngine.AI;
[HideInInspector]
public class MoveToTarget : MonoBehaviour
{
    private CharacterAI characterAI;
    private Vector3 targetpos = Vector3.zero;
    private void OnDrawGizmos()
    {
       Gizmos.DrawLine(characterAI.transform.position, targetpos);
    }
    private void Start()
    {
        characterAI = GetComponent<CharacterAI>();
    }

    public void ToDestination(GameObject target)
    {
        targetpos = target.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(characterAI.transform.position, -targetpos, out hit))
        { 
            Debug.DrawRay(characterAI.transform.position, targetpos, Color.blue);
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
        return;
    }
}