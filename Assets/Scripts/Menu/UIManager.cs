using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            _mainMenu.gameObject.set
        }
        if(GameManager.Instance.CurrentLevelName != string.Empty)
        {

        }
    }
}
