using System.Collections;
using UnityEngine;

namespace Enemy
{
	public class HasDetection : MonoBehaviour
	{
		[SerializeField] private CharacterAI characterAI;
		[SerializeField] private GameObject target;
		[SerializeField] private bool heard;
		bool go;

		private void OnTriggerEnter(Collider other)
		{
			if (!AIDirector.Instance.protectedArea)
			{
				if (other.tag == "Player")
				{
					Debug.Log("[HasDetection] PLAYER ENTER");
					target = other.gameObject;
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

		private void OnTriggerStay(Collider other)
		{
			if (other.tag == "Player" && !go)
			{
				Debug.Log("[HasDetection] PLAYER STAY");
				target = other.gameObject;
				heard = true;
				characterAI.isHearing(target, heard);
				go = true;
				StartCoroutine(StillHere());
			}
		}
		public IEnumerator StillHere()
		{
			yield return new WaitForSeconds(10f);
			go = false;
		}
	}
}