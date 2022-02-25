using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Declaring a Public Class that is accessable everywhere in project.
/// The Class Singleton is generic and will be told what type of singleton it is.
/// Singleton of this type will extend MonoBehaviour and it requires
/// that the type that is passed in is an object that is meant to 
/// extend a singleton of that same type.
/// </summary>

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance
    {
        get => instance;
    }

    public static bool isInitialized
    {
        get { return instance != null; }
    }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("[Singleton] Trying to instantiate a second instance of a singleton class");
        }
        else instance = (T)this;
    }

    protected virtual void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }
}
