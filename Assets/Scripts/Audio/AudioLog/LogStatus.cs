namespace Audio.AudioLog
{
	[System.Serializable]
	public class LogStatus
	{
		private AudioLogItem audioLog;
    
		public LogStatus(AudioLogItem logItem)
		{
			this.audioLog = logItem;
		}

		public AudioLogItem GetAudioLog()
		{
			return audioLog;
		}

	}
}
