using UnityEngine;

namespace Audio
{
	public class AutoPlay : MonoBehaviour
	{
		public AudioClip dialogueClip;

		public void PlayClip()
		{
			DialogueManager.Instance.BeginDialogue(dialogueClip);
		}
	}
}
