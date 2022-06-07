using UnityEngine;
using UnityEngine.UI;

public class LogTextUI : MonoBehaviour
{
    [SerializeField] private Text logText;

    public void Setup(LogStatus log)
    {
        logText.text = log.GetAudioLog().GetLogText();
    }
}