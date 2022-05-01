using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    //Track animation component
    //Track AnimationClips for Fade in and out
    //Function that can receive animation events
    //Function to play fade in/out animation
    
    [SerializeField] Animation _mainMenuAnimator;
    [SerializeField] AnimationClip _fadeOutAnimation;
    [SerializeField] AnimationClip _fadeInAnimation;

    [SerializeField] private Button playButton, optionsButton, creditsButton, quitButton;

    [SerializeField] private GameObject _mainMenu, _optionsMenu;
    
    private void Start()
    {
        _optionsMenu.SetActive(false);
        _mainMenu.SetActive(true);

        /*DoubleChecking that buttons Only do intended functions
         *Then adding intended function
         */
        playButton.onClick.RemoveAllListeners();
        optionsButton.onClick.RemoveAllListeners();
        creditsButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();

        playButton.onClick.AddListener(PlayGame);
        optionsButton.onClick.AddListener(ToOptionsMenu);
        creditsButton.onClick.AddListener(PlayCredits);
        quitButton.onClick.AddListener(QuitGame);
    }
    public void OnFadeOutComplete()
    {
        Debug.LogWarning("[UI Manager] FadeOut Complete");
    }
    public void OnFadeInComplete()
    {
        Debug.LogWarning("[UI Manager] FadeIn Complete");
    }

    public void FadeIn()
    {
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeInAnimation;
        _mainMenuAnimator.Play();

    }
    public void FadeOut()
    {
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeOutAnimation;
        _mainMenuAnimator.Play();
    }

    public void PlayGame()
    {
        GameManager.Instance.LoadLevel("TempCut");
        GameManager.Instance.UnloadLevel("MainMenu");
        Debug.Log("[MainMenu] Play Game");
    }
    public void QuitGame()
    {
        Debug.Log("[MainMenu] Quit Game");
        GameManager.Instance.Quit();
    }
    public void ToOptionsMenu()
    {
        _mainMenu.SetActive(false);
        _optionsMenu.SetActive(true);
    }

    public void PlayCredits()
    {
        GameManager.Instance.LoadLevel("Credits");
        GameManager.Instance.UnloadLevel("MainMenu");
    }
    public void ExitLowerMenu(GameObject menu)
    {
        menu.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void LoadLevel(string sceneName)
    {
        GameManager.Instance.LoadLevel(sceneName);
        GameManager.Instance.UnloadLevel("MainMenu");
    }
}
