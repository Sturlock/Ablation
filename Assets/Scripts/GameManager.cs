using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// The 'GameManager' tracks:
/// What level the game is currently in
/// Loads and Unloads game levels when approperate
/// Keeps track of the current game state
/// Generates persistent systems and keeps them online.
/// </summary>
public class GameManager : MonoBehaviour
{
    //public static GameManager instance;
    
    private string _currentLevelName = string.Empty;
    List<AsyncOperation> _loadOperations;

    //private void Awake()
    //{
    //    if (instance == null)
    //        instance = this;
    //    else
    //    {
    //        Destroy(gameObject);
    //        Debug.LogError("[Game Manager] Mulitple instances of this");
    //    }
    //}

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _loadOperations = new List<AsyncOperation>();
        LoadLevel("AI_Test");
    }

    private void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);
        }
        Debug.Log("Load Complete");
    }
    private void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete");
    }

    public void LoadLevel(string levelName) 
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null) { Debug.LogError("[Game Manager] Unable to load level"); return; }
        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);
        _currentLevelName = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null) {Debug.LogError("[Game Manager] Unable to unload level"); return; }
        ao.completed += OnUnloadOperationComplete;

    }

    
}