using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Ablation.Item;

public class LogList : MonoBehaviour
{
    private List<AudioLogTextItem> texts;

    public event Action onUpdate;

    public void AddLog(AudioLogTextItem log)
    {
        if (HasLog(log)) return;
        texts.Add(log);
        if (onUpdate != null) onUpdate();
    }

    private bool HasLog(AudioLogTextItem log)
    {
        return GetLogs(log) != null;
    }

    private AudioLogTextItem GetLogs(AudioLogTextItem log)
    {
        foreach (AudioLogTextItem text in texts)
        {
            if (log == text)
            {
                return log;
            }
        }
        return null;
    }
}
