using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
