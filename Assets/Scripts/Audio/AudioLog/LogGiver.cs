using UnityEngine;

namespace Audio.AudioLog
{
	public class LogGiver : MonoBehaviour
	{
		[SerializeField] private AudioLogItem audioLog;

		public void GiveLog()
		{
			LogList logList = GameObject.FindGameObjectWithTag
				("Player").GetComponent<LogList>();
			logList.AddLog(audioLog);
		}

	}
}
