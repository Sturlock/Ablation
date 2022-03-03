using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MainMenuScript _mainMenu;

    private void Start()
    {
        _mainMenu.FadeIn();
    }
}
