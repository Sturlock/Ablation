using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

/// <summary>
/// The 'GameManager' tracks:
/// What level the game is currently in
/// Loads and Unloads game levels when appropriate
/// Keeps track of the current game state
/// Generates persistent systems and keeps them online.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public GameObject[] SystemPrefabs;
    [SerializeField] private string _currentLevelName = string.Empty;

    private List<AsyncOperation> _loadOperations;
    private List<GameObject> _instancedSystemPrefrabs;

    public string CurrentLevelName => _currentLevelName;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _loadOperations = new List<AsyncOperation>();
        _instancedSystemPrefrabs = new List<GameObject>();
        LoadLevel("SplashScreen");

        InstantiateSystemPrefabs();
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            UnloadLevel(_currentLevelName);
            LoadLevel("GameLevel");
        }
    }
#endif
    private void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);
        }
        Debug.Log("[GameManager] Load Complete");
    }
    private void OnUnloadOperationComplete(AsyncOperation ao)
    {
        DialogueManager.Instance.StopDialogue();
        Debug.Log("[GameManager] Unload Complete");
    }

    private void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for(int i = 0; i <  SystemPrefabs.Length; ++i)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            _instancedSystemPrefrabs.Add(prefabInstance);
            Debug.Log("[GameManager] Instantiated " + prefabInstance.name);
        }
    }
    
    public void LoadLevel(string levelName) 
    {
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
		if (asyncOperation == null)
		{
			Debug.LogError("[Game Manager] Unable to load level"); 
			return;
		}
        asyncOperation.completed += OnLoadOperationComplete;
        _loadOperations.Add(asyncOperation);
        _currentLevelName = levelName;
    }

    internal void Quit()
    {
        Application.Quit();
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