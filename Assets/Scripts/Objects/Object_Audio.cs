using System.Collections;
using UnityEngine;

namespace Objects
{
	public class Object_Audio : MonoBehaviour
	{
		[SerializeField] private AudioSource source;
		[SerializeField] private SphereCollider sphere;
		public bool play = false;

		// Start is called before the first frame update
		private void Start()
		{
			source = GetComponent<AudioSource>();
			sphere = gameObject.AddComponent<SphereCollider>();
			source.playOnAwake = false;
			sphere.enabled = false;
			sphere.radius = source.maxDistance;
			sphere.isTrigger = true;
		}

		public IEnumerator PlaySound()
		{
			source.Play();
			sphere.enabled = true;
			yield return new WaitForSeconds(.1f);
			play = false;
			sphere.enabled = false;
		}
	}
}