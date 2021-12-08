using TMPro;
using UnityEngine;

public class HandyMan : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI contence;

    private void Start()
    {
        title.text = "HandyMan";
        contence.text = "HandyMan: Home";
    }

    public void Status()
    {
        title.text = "Status";
        contence.text = "Status Stuff";
    }

    public void Inventory()
    {
        title.text = "Inventory";
        contence.text = "Inventory Stuff";
    }

    public void Options()
    {
        title.text = "Options";
        contence.text = "Options Stuff";
    }

    public void AudioLogs()
    {
        title.text = "AudioLogs";
        contence.text = "AudioLogs Stuff";
    }

    public void MotionDetector()
    {
        title.text = "Motion Detector";
        contence.text = "Motion Detector Stuff";
    }
}