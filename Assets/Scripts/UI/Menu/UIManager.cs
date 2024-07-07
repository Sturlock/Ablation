using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menu
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField] private MainMenuScript _MainMenu;
		[SerializeField] private GameObject _HUD;

		[SerializeField] private String _MainMenuLevel;
		[SerializeField] private String _GameLevel;

		private void Start()
		{
			List<String> sceneNames = new List<String>();
			Int32 sceneCount = SceneManager.sceneCountInBuildSettings;
			for (Int32 buildIndex = 0; buildIndex < sceneCount; buildIndex++)
			{
				String sceneName = SceneUtility.GetScenePathByBuildIndex(buildIndex);
				
				sceneNames.Add(sceneName);
			}

			Boolean levelExists = sceneNames.Exists(scene => scene.Contains(_MainMenuLevel));
			if (!levelExists)
			{
				Debug.LogError($"{_MainMenuLevel} is not a currently valid scene name");
			}

			levelExists = sceneNames.Exists(scene => scene.Contains(_GameLevel));
			if (!levelExists)
			{
				Debug.LogError($"{_GameLevel} is not a currently valid scene name");
			}
		}

		private void Update()
		{
			if (GameManager.Instance.CurrentLevelName == _MainMenuLevel)
			{
				_HUD.SetActive(false);
				_MainMenu.gameObject.SetActive(true);
			}
			else if (GameManager.Instance.CurrentLevelName == _GameLevel)
			{
				_MainMenu.gameObject.SetActive(false);
				_HUD.SetActive(true);
			}
			else if ((GameManager.Instance.CurrentLevelName != "MainMenu") ||
			         (GameManager.Instance.CurrentLevelName != "GameLevel"))
			{
				_MainMenu.gameObject.SetActive(false);
				_HUD.SetActive(false);
			}
		}
	}
}