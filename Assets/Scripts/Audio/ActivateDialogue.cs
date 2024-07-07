using Interface;
using Player;
using UI;
using UnityEngine;

namespace Audio
{
	public class ActivateDialogue : MonoBehaviour, IInteractable
	{
		public AudioClip dialogueClip;
		public Animator ani;
		public bool played = false;
		public bool Inside = false;
		public GameObject target;
		public LayerMask _layer;

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(target.transform.position, 1.5f);
		}
		public void Action(PlayerInteract script)
		{
			throw new System.NotImplementedException();
		}

		public void Interact(PlayerInteract script)
		{
			played = false;
			ani.SetBool("Help", true);
			FindObjectOfType<ShowHideHandy>().can = true;
			DialogueManager.Instance.BeginDialogue(dialogueClip);
			gameObject.SetActive(false);

		}

		private void FixedUpdate()
		{
			if (Physics.CheckSphere(target.transform.position, 1.5f, _layer))
			{
				if (!played)
				{
					ani.SetBool("Activate", true);
				}
			}
			else
			{
				ani.SetBool("Activate", false);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player") && !played)
			{
            
            
            
			}
		}
		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				ani.SetBool("Activate", false);
            

			}
		}
	}
}
