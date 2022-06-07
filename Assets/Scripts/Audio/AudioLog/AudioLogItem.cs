using UnityEngine;

[CreateAssetMenu(fileName = "AudioLogItem", menuName = "Ablation/audioLog")]
public class AudioLogItem : ScriptableObject
{
    public string logName;
    public AudioClip log;
    public string text;

    public string GetLogName()
    {
        return logName;
    }

    public string GetLogText()
    {
        return text;
    }

    public AudioClip GetLogAudio()
    {
        return log;
    }

    public static AudioLogItem GetByName(string logName)
    {
        foreach (AudioLogItem logItem in UnityEngine.Resources.LoadAll<AudioLogItem>(""))
        {
            if (logItem.logName == logName)
            {
                return logItem;
            }
        }
        return null;
    }
}