using UnityEngine;

namespace Objects
{
	public class Shutters : MonoBehaviour
	{
		public Animator animator;
		// Start is called before the first frame update
		private void OnTriggerEnter(Collider other)
		{
			animator.SetTrigger("Open");
		}
	}
}
