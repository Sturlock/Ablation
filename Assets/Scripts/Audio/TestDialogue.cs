using UnityEngine;

namespace Audio
{
	public class TestDialogue : MonoBehaviour
	{
		public AudioClip dialogueClip;
		public bool played = false;
		public void OnClick()
		{
			if (!played)
			{
				DialogueManager.Instance.BeginDialogue(dialogueClip);
				played = true;
			}
		}


		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player") && !played)
			{
				DialogueManager.Instance.BeginDialogue(dialogueClip);
				played = true;
			}
		}
	}
}