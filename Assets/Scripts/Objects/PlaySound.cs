using UnityEngine;

namespace Objects
{
	public class PlaySound : MonoBehaviour
	{
		private ObjectAudio GetAudio;
		public bool play = false;

		// Start is called before the first frame update
		private void Start()
		{
			GetAudio = GetComponentInChildren<ObjectAudio>();
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