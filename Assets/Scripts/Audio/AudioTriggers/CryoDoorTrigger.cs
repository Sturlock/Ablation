using UnityEngine;

namespace Audio.AudioTriggers
{
	public class CryoDoorTrigger : MonoBehaviour
	{
		public bool pickup = false;
		public AudioClip audioClip;
		private AudioSource audioSource;
		public Animator animator;

		private void Start()
		{
			audioSource = GetComponent<AudioSource>();
			audioSource.clip = audioClip;
		}
		public void CryoDoorOpen()
		{
			pickup = true;
			audioSource.Play();
			animator.SetBool("Open", pickup);
		}
	}
}
