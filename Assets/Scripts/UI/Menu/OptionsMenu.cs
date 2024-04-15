using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
	public class OptionsMenu : MonoBehaviour
	{
		[SerializeField]
		MainMenuScript _exit;
		[SerializeField]
		private Button graphicsButton, soundButton, exitButton;
		[SerializeField]
		private GameObject _optionsMenu, _graphicsMenu, _soundMenu;

		private void Start()
		{
			_optionsMenu.SetActive(false);
			_graphicsMenu.SetActive(false);
			_soundMenu.SetActive(false);

			graphicsButton.onClick.RemoveAllListeners();
			soundButton.onClick.RemoveAllListeners();
			exitButton.onClick.RemoveAllListeners();

			graphicsButton.onClick.AddListener(ToGraphicsMenu);
			soundButton.onClick.AddListener(ToSoundMenu);
			exitButton.onClick.AddListener(ExitMenu);
		}

		public void ToGraphicsMenu()
		{
			_optionsMenu.SetActive(false);
			_graphicsMenu.SetActive(true);
		}
		public void ToSoundMenu()
		{
			_optionsMenu.SetActive(false);
			_soundMenu.SetActive(true);
		}

		public void ExitLowerMenu(GameObject menu)
		{
			menu.SetActive(false);
			_optionsMenu.SetActive(true);
		}

		void ExitMenu()
		{
			_exit.ExitLowerMenu(_optionsMenu);
		}
	}
}
