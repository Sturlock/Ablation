using UnityEngine;

namespace Audio.AudioTriggers
{
	public class AllenDoorTrigger : MonoBehaviour
	{
		public void Activate()
		{
			gameObject.GetComponent<SphereCollider>().enabled = true;
		}

	}
}
