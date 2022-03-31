using UnityEngine;
using UnityEngine.UI;

public class HandyMan : MonoBehaviour
{
    private SecurityClearance lvl;
    public Image lvlColour;
    [SerializeField] private Text _title;
    [SerializeField] private GameObject[] _contences;
    [SerializeField] private CanvasGroup _canvasGroup;
    private ShowHideHandy showHide;

    private void Start()
    {
        lvl = FindObjectOfType<SecurityClearance>();
        showHide = GetComponent<ShowHideHandy>();
        _canvasGroup.alpha = 0f;

        Handy();
    }

    private void Update()
    {
        if (showHide.Show)
        {
            Handy();
        }
    }

    public void Handy()
    {
        CloseAll();
        _title.text = "HandyMan";
    }

    public void Status()
    {
        CloseAll();
        _title.text = "Status";
        _contences[0].SetActive(true);

        int i = (int)lvl.GetCurrentLevel;
        switch (i)
        {
            case 0:
                lvlColour.color = Color.white;
                break;
            case 1: 
                lvlColour.color = Color.blue;
                break;
            default: 
                lvlColour.color = Color.white;
                break;
        }
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
        foreach (GameObject go in _contences)
        {
            go.SetActive(false);
        }
    }
}