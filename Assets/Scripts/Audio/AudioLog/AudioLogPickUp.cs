using Interface;
using Player;
using UnityEngine;

namespace Audio.AudioLog
{
	public class AudioLogPickUp : MonoBehaviour, IInteractable
	{
		[SerializeField] private LogGiver lg;

		private void Start()
		{
			lg = GetComponent<LogGiver>();
		}
		public void Action(PlayerInteract script)
		{
			throw new System.NotImplementedException();
		}

		public void Interact(PlayerInteract script)
		{
			lg.GiveLog();
			Destroy(gameObject);
		}
	}
}
