using TMPro;
using UnityEngine;

public class HandyMan : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private GameObject[] contences;

    private void Start()
    {
        title.text = "HandyMan";
        contences[0].SetActive(true);
    }

    public void Status()
    {
        title.text = "Status";
        contences[1].SetActive(true); 
    }

    public void Inventory()
    {
        title.text = "Inventory";
        contences[2].SetActive(true);
    }

    public void Options()
    {
        title.text = "Options";
        contences[3].SetActive(true);
    }

    public void AudioLogs()
    {
        title.text = "AudioLogs";
        contences[4].SetActive(true);
    }

    public void MotionDetector()
    {
        title.text = "Motion Detector";
        contences[5].SetActive(true);
    }
}