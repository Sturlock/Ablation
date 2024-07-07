using UnityEngine;

namespace Scenes._EllenTestBox.NOGSplashscreen
{
	public class LoadSceneAnimation : MonoBehaviour
	{
		public string SceneNow;
		public string SceneNext;

		public void Update()
		{
			//if (Input.GetKeyUp(KeyCode.Escape))
			//{
			//    GameManager.Instance.LoadLevel(SceneNext);
			//    GameManager.Instance.UnloadLevel(SceneNow);
			//}
		}
		public void LoadNewScene()
		{
			Cursor.lockState = CursorLockMode.None;
			GameManager.Instance.LoadLevel(SceneNext);
			GameManager.Instance.UnloadLevel(SceneNow);
			Debug.Log("[LoadSceneAnimation] " + SceneNow + "going to" + SceneNext);
			Time.timeScale = 1f;
			AudioListener.pause = false;
		}

	}
}
