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
        title.text = "Status";
        contence.text = "Status Stuff";
    }

    public void Options()
    {
        title.text = "Status";
        contence.text = "Status Stuff";
    }

    public void AudioLogs()
    {
        title.text = "Status";
        contence.text = "Status Stuff";
    }

    public void MotionDetector()
    {
        title.text = "Status";
        contence.text = "Status Stuff";
    }
}