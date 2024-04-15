using UnityEngine;

namespace Objects
{
	public class PlaySound : MonoBehaviour
	{
		private Object_Audio GetAudio;
		public bool play = false;

		// Start is called before the first frame update
		private void Start()
		{
			GetAudio = GetComponentInChildren<Object_Audio>();
		}

		// Update is called once per frame
		private void Update()
		{
			if (play)
			{
				StartCoroutine(GetAudio.PlaySound());
				play = false;
			}
		}
	}
}