using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressToSkip : MonoBehaviour
{
    public string SceneNow;
    public string SceneNext;

    public void Update()
    {
        if (Input.anyKey)
        {
            GameManager.Instance.LoadLevel(SceneNext);
            GameManager.Instance.UnloadLevel(SceneNow);
        }
    }
}
