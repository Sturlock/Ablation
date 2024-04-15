using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
	public class OptionsMenu : MonoBehaviour
	{
		[SerializeField] private MainMenuScript _Exit;
		[SerializeField] private Button _GraphicsButton;
		[SerializeField] private Button _SoundButton;
		[SerializeField] private Button _ExitButton;
		[SerializeField] private GameObject _OptionsMenu;
		[SerializeField] private GameObject _GraphicsMenu;
		[SerializeField] private GameObject _SoundMenu;

		private void Start()
		{
			_OptionsMenu.SetActive(false);
			_GraphicsMenu.SetActive(false);
			_SoundMenu.SetActive(false);

			_GraphicsButton.onClick.RemoveAllListeners();
			_SoundButton.onClick.RemoveAllListeners();
			_ExitButton.onClick.RemoveAllListeners();

			_GraphicsButton.onClick.AddListener(ToGraphicsMenu);
			_SoundButton.onClick.AddListener(ToSoundMenu);
			_ExitButton.onClick.AddListener(ExitMenu);
		}

		public void ToGraphicsMenu()
		{
			_OptionsMenu.SetActive(false);
			_GraphicsMenu.SetActive(true);
		}

		public void ToSoundMenu()
		{
			_OptionsMenu.SetActive(false);
			_SoundMenu.SetActive(true);
		}

		public void ExitLowerMenu(GameObject menu)
		{
			menu.SetActive(false);
			_OptionsMenu.SetActive(true);
		}

		private void ExitMenu()
		{
			_Exit.ExitLowerMenu(_OptionsMenu);
		}
	}
}