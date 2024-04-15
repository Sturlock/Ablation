using Audio;
using Interface;
using Player;
using UnityEngine;

namespace Objects
{
	public class Key_Card : MonoBehaviour, IInteractable
	{
		private SecurityClearance security;
		public AudioClip dialogueClip;

		private void Awake()
		{
			security = FindObjectOfType<SecurityClearance>();
		}

		public void Action(PlayerInteract script)
		{
			throw new System.NotImplementedException();
		}

		public void Interact(PlayerInteract script)
		{
			security.IncreaseLevel();
			DialogueManager.Instance.BeginDialogue(dialogueClip);
			Destroy(gameObject);
		}
	}
}
