using System;
using UnityEngine;

namespace Utils
{
	/// <summary>
	/// Declaring a Public Class that is accessable everywhere in project.
	/// The Class Singleton is generic and will be told what type of singleton it is.
	/// Singleton of this type will extend MonoBehaviour and it requires
	/// that the type that is passed in is an object that is meant to 
	/// extend a singleton of that same type.
	/// </summary>

	public class Singleton<T> : MonoBehaviour where T : Singleton<T>
	{
		private static T _Instance;

		public static T Instance => _Instance;

		public static Boolean IsInitialized => _Instance;

		protected virtual void Awake()
		{
			if (_Instance)
			{
				Debug.LogError("[Singleton] Trying to instantiate a second instance of a singleton class");
			}
			else _Instance = (T)this;
		}

		protected virtual void OnDestroy()
		{
			if(_Instance == this)
			{
				_Instance = null;
			}
		}
	}
}
