using UnityEngine;

namespace Objects
{
	public class AutoDoor : MonoBehaviour
	{
		[SerializeField] private Animator ani;
		[SerializeField] private bool doorOpen;
		[SerializeField] private GameObject target;
		[SerializeField] private AudioSource aud;
		public AudioClip Open;
		public AudioClip Close;

		private void Awake()
		{
			ani = GetComponentInChildren<Animator>();
			aud = GetComponent<AudioSource>();
		}

		private void OpenDoor(bool open)
		{
			ani.SetBool("Open", open);
			if (open)
			{
				aud.clip = Open;
				aud.PlayOneShot(Open);
			}
			else if (!open)
			{
				aud.clip = Close;
				aud.PlayOneShot(Close);
			}
        
		}


		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player") || other.CompareTag("Monster"))
			{
				doorOpen = true;
				target = other.gameObject;
				OpenDoor(doorOpen);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Player") || other.CompareTag("Monster"))
			{
				doorOpen = false;
				target = null;
				OpenDoor(doorOpen);
			}
		}
	}
}