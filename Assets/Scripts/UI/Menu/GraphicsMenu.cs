using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
	public class GraphicsMenu : MonoBehaviour
	{
		[SerializeField] private GameObject _GraphicsMenu;
		[SerializeField] private TMP_Dropdown _Fullscreen;
		[SerializeField] private TMP_Dropdown _ResolutionDropdown;
		[SerializeField] private TMP_Dropdown _QualityLevel;

		[SerializeField] private OptionsMenu _Exit;
		[SerializeField] private Button _ExitButton;


		private Resolution[] _Resolutions;

		private void Awake()
		{
			_Fullscreen.onValueChanged.RemoveAllListeners();
			_QualityLevel.onValueChanged.RemoveAllListeners();
			_ResolutionDropdown.onValueChanged.RemoveAllListeners();
			_ExitButton.onClick.RemoveAllListeners();

			_Fullscreen.onValueChanged.AddListener(SetFullscreen);
			_QualityLevel.onValueChanged.AddListener(SetQuality);
			_ResolutionDropdown.onValueChanged.AddListener(SetResolution);
			_ExitButton.onClick.AddListener(ExitMenu);
		}

		private void Start()
		{
			_Fullscreen.value = Screen.fullScreenMode switch
			{
				FullScreenMode.ExclusiveFullScreen => 0,
				FullScreenMode.FullScreenWindow => 1,
				FullScreenMode.MaximizedWindow => 2,
				FullScreenMode.Windowed => 3,
				_ => 0
			};

			//Getting available resolutions and applying them to the dropdown
			_Resolutions = Screen.resolutions;
			_ResolutionDropdown.ClearOptions();
			List<String> options = new List<String>();
			Int32 currentResolutionIndex = 0;
			for (Int32 i = 0; i < _Resolutions.Length; i++)
			{
				String option = $"{_Resolutions[i].width} x {_Resolutions[i].height} - {_Resolutions[i].refreshRateRatio.value}Hz";
				options.Add(option);

				if (_Resolutions[i].width == Screen.currentResolution.width &&
				    _Resolutions[i].height == Screen.currentResolution.height)
				{
					currentResolutionIndex = i;
				}
			}

			_ResolutionDropdown.AddOptions(options);
			_ResolutionDropdown.value = currentResolutionIndex;
			_ResolutionDropdown.RefreshShownValue();
			_QualityLevel.value = QualitySettings.GetQualityLevel();
		}

		public void SetResolution(Int32 resolutionIndex)
		{
			Resolution resolution = _Resolutions[resolutionIndex];
			Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
		}

		public static void SetQuality(Int32 qualityIndex)
		{
			QualitySettings.SetQualityLevel(qualityIndex);
		}

		public static void SetFullscreen(Int32 fullscreenType)
		{
			Screen.fullScreenMode = fullscreenType switch
			{
				0 => FullScreenMode.ExclusiveFullScreen,
				1 => FullScreenMode.FullScreenWindow,
				2 => FullScreenMode.MaximizedWindow,
				3 => FullScreenMode.Windowed,
				_ => FullScreenMode.ExclusiveFullScreen
			};
		}

		private void ExitMenu()
		{
			_Exit.ExitLowerMenu(_GraphicsMenu);
		}
	}
}