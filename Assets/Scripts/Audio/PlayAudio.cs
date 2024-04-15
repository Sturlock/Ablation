using UnityEngine;

namespace Audio
{
	public class PlayAudio : MonoBehaviour
	{
		//[SerializeField] private Animator ani;
		[SerializeField] private bool Fade;
		[SerializeField] private bool Hit;
		//[SerializeField] private GameObject target;
		[SerializeField] private AudioSource aud;
		//public AudioClip playClip;
		public float volumelevel = 0f;
		public float maxVol = .5f;

		private void Awake()
		{
			//ani = GetComponent<Animator>();
			aud = GetComponent<AudioSource>();
			aud.volume = volumelevel;
		}

		private void Update()
		{
			if (Fade && (volumelevel < maxVol))
			{
				volumelevel += .005f;
				aud.volume = volumelevel;
			}
			if (!Fade && (volumelevel != 0))
			{
				volumelevel -= .005f;
				aud.volume = volumelevel;
				if (volumelevel < 0)
				{
					volumelevel = 0;
				}
			}

		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				//if (!Hit)
				//{
				//    aud.PlayOneShot(playClip);
				//}
				Fade = true;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				Fade = false;
			}
		}
	}
}
