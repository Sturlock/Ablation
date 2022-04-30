using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphicsMenu : MonoBehaviour
{
    [SerializeField] GameObject _graphicsMenu;
    [SerializeField] TMP_Dropdown fullscreen;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] TMP_Dropdown qualityLevel;

    [SerializeField] OptionsMenu _exit;
    [SerializeField] Button exitButton;
    

    Resolution[] resolutions;

    private void Awake()
    {
        fullscreen.onValueChanged.RemoveAllListeners();
        qualityLevel.onValueChanged.RemoveAllListeners();
        resolutionDropdown.onValueChanged.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();

        fullscreen.onValueChanged.AddListener(SetFullscreen);
        qualityLevel.onValueChanged.AddListener(SetQuality);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        exitButton.onClick.AddListener(ExitMenu);
    }

    private void Start()
    {
        switch (Screen.fullScreenMode)
        {
            case FullScreenMode.ExclusiveFullScreen:
                fullscreen.value = 0; break;
            case FullScreenMode.FullScreenWindow:
                fullscreen.value = 1; break;
            case FullScreenMode.MaximizedWindow:
                fullscreen.value = 2; break;
            case FullScreenMode.Windowed:
                fullscreen.value = 3; break;
            default:
                fullscreen.value = 0; break;


        }

        //Getting available resolutions and applying them to the dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " - " + resolutions[i].refreshRate + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (int FullscreenType)
    {
        switch (FullscreenType)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
            case 3:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            default:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
        }
        
    }
    void ExitMenu()
    {
        _exit.ExitLowerMenu(_graphicsMenu);
    }
}
