using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioLogUI : MonoBehaviour
{
    [SerializeField] private Text title;
   
    private LogStatus log;

    private void Start()
    {
        
    }
    public void Setup(LogStatus log)
    {
        this.log = log;
        title.text = log.GetAudioLog().GetLogName();
        LogTextUI logText = FindObjectOfType<LogList>().logText;
        logText.Setup(log);
    }

    public LogStatus GetCurrentLog()
    {
        return log;
    }
}