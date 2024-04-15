using UnityEngine;
using UnityEngine.UI;

namespace Audio.AudioLog
{
	public class LogTextUI : MonoBehaviour
	{
		[SerializeField] private Text logText;

		public void Setup(LogStatus log)
		{
			logText.text = log.GetAudioLog().GetLogText();
		}
	}
}