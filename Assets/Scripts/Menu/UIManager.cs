using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MainMenuScript _mainMenu;
    
    private void Start()
    {
        _mainMenu.FadeIn();
    }
    private void Update()
    {
        if (GameManager.Instance.CurrentLevelName == "MainMenu")
        {
            _mainMenu.gameObject.SetActive(true);
           
        }
        if (GameManager.Instance.CurrentLevelName != "MainMenu")
        {
            _mainMenu.gameObject.SetActive(false);
            
        }
    }
    
}
