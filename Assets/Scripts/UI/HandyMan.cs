using System;
using UnityEngine;
using UnityEngine.UI;

public class HandyMan : MonoBehaviour
{
    private SecurityClearance lvl;
    public Image lvlColour;
    
    [SerializeField] private Text _title;
    [SerializeField] private Button[] _hmButtons; 
    [SerializeField] private GameObject[] _contences;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button[] _optionsButtons;
    [SerializeField] private Button[] _quitButtons;
    public ShowHideHandy showHide;

    private void Start()
    {
        lvl = FindObjectOfType<SecurityClearance>();
        _canvasGroup.alpha = 0f;

        #region ButtonSetUp
        _hmButtons[0].onClick.AddListener(Status);
        _hmButtons[1].onClick.AddListener(Inventory);
        _hmButtons[2].onClick.AddListener(AudioLogs);
        _hmButtons[3].onClick.AddListener(Options);

        _optionsButtons[0].onClick.AddListener(Sound);
        _optionsButtons[1].onClick.AddListener(Graphics);
        _optionsButtons[2].onClick.AddListener(Quit);

        _quitButtons[0].onClick.AddListener(QuitGame);
        _quitButtons[1].onClick.AddListener(Options);
        #endregion
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

#region Main Menu
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
    #endregion

#region Options Menu
    private void Sound()
    {
        throw new NotImplementedException();
    }
    private void Graphics()
    {
        throw new NotImplementedException();
    }

    private void Quit()
    {
        CloseAll();
        _contences[4].SetActive(true);
    }

    private void QuitGame()
    {
        GameManager.Instance.LoadLevel("MainMenu");
        GameManager.Instance.UnloadLevel("GameLevel");

    }
    #endregion

    private void CloseAll()
    {
        foreach (GameObject go in _contences)
        {
            go.SetActive(false);
        }
    }

    
}