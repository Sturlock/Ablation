using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    //Track animation component
    //Track AnimationClips for Fade in and out
    //Function that can receive animation events
    //Function to play fade in/out animation
    
    [SerializeField] Animation _mainMenuAnimator;
    [SerializeField] AnimationClip _fadeOutAnimation;
    [SerializeField] AnimationClip _fadeInAnimation;

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
}
