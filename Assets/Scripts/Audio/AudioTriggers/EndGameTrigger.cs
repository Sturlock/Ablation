using UnityEngine;

namespace Audio.AudioTriggers
{
	public class EndGameTrigger : MonoBehaviour
	{
		public Animator animator;

		public void EndGameFade()
		{
			animator.SetBool("EndGame", true);
		}
	}
}
