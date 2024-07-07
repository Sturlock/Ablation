using UnityEngine;

namespace Scenes._EllenTestBox.Mainmenu
{
	public class LoadingBar : MonoBehaviour
	{
		public Animator Loading;
    
		public void Triggered()
		{
			Loading.SetBool("Loading", true);
		}
	}
}
