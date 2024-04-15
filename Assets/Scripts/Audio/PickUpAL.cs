using Audio.AudioLog;
using Interface;
using Player;
using UnityEngine;

namespace Audio
{
	public class PickUpAL : MonoBehaviour, IInteractable
	{
		public AudioLogItem textLog;
		public AudioLogUI Ctrl;

		// Start is called before the first frame update
		void Start()
		{
			Ctrl = FindObjectOfType<AudioLogUI>();
		}

		public void Action(PlayerInteract script)
		{
			throw new System.NotImplementedException();
		}

		public void Interact(PlayerInteract script)
		{
			LogList logList = GameObject.FindGameObjectWithTag("Player").GetComponent<LogList>();
			logList.AddLog(textLog);
			Debug.Log("[PickUpAL] DESTROY ME! HAHAHAHAHA");
		}
		// Update is called once per frame
		void Update()
		{
        
		}
	}
}
