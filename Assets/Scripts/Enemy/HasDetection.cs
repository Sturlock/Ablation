using UnityEngine;

public class HasDetection : MonoBehaviour
{
    [SerializeField] private CharacterAI characterAI;
    [SerializeField] private GameObject target;
    [SerializeField] private bool heard;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("PLAYER");
            target = other.transform.parent.gameObject;
            heard = true;
            characterAI.IsHeard(target, heard);
        }
        if (other.tag == "SOUND")
        {
            target = other.gameObject;
            heard = true;
        }
    }
}