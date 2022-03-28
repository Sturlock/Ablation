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

    [SerializeField] private Button _button1, _button2;

    public Button PlayButton
    {
        get { return _button1; }
    }

    public Button QuitButton
    {
        get { return _button2; }
    }
    private void Start()
    {
        PlayButton.onClick.RemoveAllListeners();
        PlayButton.onClick.AddListener(PlayGame);
        QuitButton.onClick.RemoveAllListeners();
        QuitButton.onClick.AddListener(QuitGame);
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

    public void LoadLevel(string sceneName)
    {
        GameManager.Instance.LoadLevel(sceneName);
        GameManager.Instance.UnloadLevel("MainMenu");
    }
}
