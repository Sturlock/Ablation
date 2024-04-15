using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class Crosshair : MonoBehaviour
	{
		[SerializeField] private Image _crosshair;

		public Image crosshair
		{
			get { return _crosshair; }
		}
	}
}
