using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MainMenuScript _mainMenu;
    [SerializeField] private GameObject _hud;
    
    private void Start()
    {
        //_mainMenu.FadeIn();
    }
    private void Update()
    {
        if (GameManager.Instance.CurrentLevelName == "MainMenu")
        {
            _hud.SetActive(false);
            _mainMenu.gameObject.SetActive(true);
            
        }
        else if (GameManager.Instance.CurrentLevelName == "GameLevel")
        {
            _mainMenu.gameObject.SetActive(false);
            _hud.SetActive(true);
        }
        else if ((GameManager.Instance.CurrentLevelName != "MainMenu") || 
            (GameManager.Instance.CurrentLevelName != "GameLevel"))
        {
            _mainMenu.gameObject.SetActive(false);
            _hud.SetActive(false);

        }
    }
    
}
