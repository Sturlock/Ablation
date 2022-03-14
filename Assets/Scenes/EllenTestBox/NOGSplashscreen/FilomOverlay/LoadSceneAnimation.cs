using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAnimation : MonoBehaviour
{
    public string SceneName;

    public void LoadNewScene()
    {
        SceneManager.LoadScene(SceneName);
    }

}
