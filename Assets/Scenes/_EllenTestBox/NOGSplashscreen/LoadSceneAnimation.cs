using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        GameManager.Instance.LoadLevel(SceneNext);
        GameManager.Instance.UnloadLevel(SceneNow);
        Debug.Log("[LoadSceneAnimation] " + SceneNow + "going to" + SceneNext);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

}
