using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LogList : MonoBehaviour
{
    public List<LogStatus> logs = new List<LogStatus>();
    public LogTextUI logText;
    public event Action onUpdate;

    public void AddLog(AudioLogItem log)
    {
        if (HasLog(log)) return;

        LogStatus logStatus = new LogStatus(log);
        logs.Add(logStatus);
        if (onUpdate != null) onUpdate();
    }

    private bool HasLog(AudioLogItem log)
    {
        return GetAudioLogs(log) != null;
    }

    public IEnumerable<LogStatus> GetLogs()
    {
        return logs;
    }

    private LogStatus GetAudioLogs(AudioLogItem log)
    {
        foreach (LogStatus status in logs)
        {
            if(status.GetAudioLog() == log)
            {
                return status;
            }
        }
        return null;
    }
}
