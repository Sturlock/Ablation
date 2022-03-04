using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MainMenuScript _mainMenu;
    [SerializeField] private HandyMan _handyMan;
    

    private void Start()
    {
        _mainMenu.FadeIn();
    }
    private void Update()
    {
        if (GameManager.Instance.CurrentLevelName == "MainMenu")
        {
            _mainMenu.gameObject.SetActive(true);
            _handyMan.gameObject.SetActive(false);
        }
        if (GameManager.Instance.CurrentLevelName != "MainMenu")
        {
            _mainMenu.gameObject.SetActive(false);
            _handyMan.gameObject.SetActive(true);
        }
    }
    
}
