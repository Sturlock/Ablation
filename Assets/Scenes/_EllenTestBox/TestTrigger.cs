using UnityEngine;

namespace Scenes._EllenTestBox
{
	public class testTrigger : MonoBehaviour
	{
		public GameObject _light;

		private void Start()
		{
			_light.SetActive(false);
		}
		public void Grab()
		{
			_light.SetActive(true);
		}

		public void Poof()
		{
			_light.SetActive(false);
		}
	}
}
