using UnityEngine;
using UnityEngine.UI;

public class HandyMan : MonoBehaviour
{
    
    [SerializeField] private Text _title;
    [SerializeField] private GameObject[] _contences;
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup.alpha = 0f;
        CloseAll();
        _title.text = "HandyMan";
    }

    public void Status()
    {
        CloseAll();
        _title.text = "Status";
        _contences[0].SetActive(true);
    }

    public void Inventory()
    {
        CloseAll();
        _title.text = "Inventory";
        _contences[1].SetActive(true);
    }

    public void AudioLogs()
    {
        CloseAll();
        _title.text = "AudioLogs";
        _contences[2].SetActive(true);
    }

    public void Options()
    {
        CloseAll();
        _title.text = "Options";
        _contences[3].SetActive(true);
    }

    public void MotionDetector()
    {
        CloseAll();
        _title.text = "Motion Detector";
        //contences[].SetActive(true);
    }

    private void CloseAll()
    {
        foreach(GameObject go in _contences)
        {
            go.SetActive(false);
        }
    }
}