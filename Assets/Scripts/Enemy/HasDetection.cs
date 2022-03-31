using UnityEngine;

public class HasDetection : MonoBehaviour
{
    [SerializeField] private CharacterAI characterAI;
    [SerializeField] private GameObject target;
    [SerializeField] private bool heard;

    private void OnTriggerEnter(Collider other)
    {
        if (!AIDirector.Instance.protectedArea)
        {
	        if (other.transform.parent.tag == "Player")
	        {
	            Debug.Log("[HasDetection] PLAYER ENTER");
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

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.transform.parent.tag == "Player")
    //    {
    //        Debug.Log("[HasDetection] PLAYER STAY");
    //        target = other.transform.parent.gameObject;
    //        heard = true;
    //        characterAI.isHearing(target, heard);
    //    }
    //}
}