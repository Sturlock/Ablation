using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
	public class MainMenuScript : MonoBehaviour
	{
		//Track animation component
		//Track AnimationClips for Fade in and out
		//Function that can receive animation events
		//Function to play fade in/out animation

		[SerializeField] private Animation _MainMenuAnimator;
		[SerializeField] private AnimationClip _FadeOutAnimation;
		[SerializeField] private AnimationClip _FadeInAnimation;

		[SerializeField] private Button _PlayButton;
		[SerializeField] private Button _OptionsButton;
		[SerializeField] private Button _CreditsButton;
		[SerializeField] private Button _QuitButton;

		[SerializeField] private GameObject _MainMenu;
		[SerializeField] private GameObject _OptionsMenu;

		private void Start()
		{
			_OptionsMenu.SetActive(false);
			_MainMenu.SetActive(true);

			/*DoubleChecking that buttons Only do intended functions
			 *Then adding intended function
			 */
			_PlayButton.onClick.RemoveAllListeners();
			_OptionsButton.onClick.RemoveAllListeners();
			_CreditsButton.onClick.RemoveAllListeners();
			_QuitButton.onClick.RemoveAllListeners();

			_PlayButton.onClick.AddListener(PlayGame);
			_OptionsButton.onClick.AddListener(ToOptionsMenu);
			_CreditsButton.onClick.AddListener(PlayCredits);
			_QuitButton.onClick.AddListener(QuitGame);
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
			_MainMenuAnimator.Stop();
			_MainMenuAnimator.clip = _FadeInAnimation;
			_MainMenuAnimator.Play();
		}

		public void FadeOut()
		{
			_MainMenuAnimator.Stop();
			_MainMenuAnimator.clip = _FadeOutAnimation;
			_MainMenuAnimator.Play();
		}

		public void PlayGame()
		{
			GameManager.Instance.LoadLevel("TempCutScene");
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
			_MainMenu.SetActive(false);
			_OptionsMenu.SetActive(true);
		}

		public void PlayCredits()
		{
			GameManager.Instance.LoadLevel("Credits");
			GameManager.Instance.UnloadLevel("MainMenu");
		}

		public void ExitLowerMenu(GameObject menu)
		{
			menu.SetActive(false);
			_MainMenu.SetActive(true);
		}

		public void LoadLevel(String sceneName)
		{
			GameManager.Instance.LoadLevel(sceneName);
			GameManager.Instance.UnloadLevel("MainMenu");
		}
	}
}