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
public class GameManager : Singleton<GameManager>
{
    public GameObject[] SystemPrefabs;
    private string _currentLevelName = string.Empty;
    
    List<AsyncOperation> _loadOperations;
    List<GameObject> _instancedSystemPrefrabs;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _loadOperations = new List<AsyncOperation>();
        LoadLevel("AI_Test");

        InstantiateSystemPrefabs();
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

    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for(int i = 0; i <SystemPrefabs.Length; ++i)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            _instancedSystemPrefrabs.Add(prefabInstance);
            Debug.Log("[GameManager] Instantiated " + prefabInstance.name);
        }
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

    protected override void OnDestroy()
    {
        base.OnDestroy();
        for(int i =0; i<_instancedSystemPrefrabs.Count; ++i)
        {
            Destroy(_instancedSystemPrefrabs[i]);
        }
        _instancedSystemPrefrabs.Clear();
    }

}