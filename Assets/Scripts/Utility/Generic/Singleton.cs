using System.Collections;
using System.Collections.Generic;
using Utility.Attribute;
using System;
using UnityEngine;
using UnityEngine.Serialization;
// ReSharper disable StaticMemberInGenericType

namespace Utility.Generic
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
		private static T _instance;

		public static T Instance {
			get {
				if (!Instantiated) CreateInstance();
				return _instance;
			}
		}

		private static void CreateInstance() {
			if (Destroyed) return;
			
			var type = typeof (T);
			var objects = FindObjectsOfType<T>();
			
			if (objects.Length > 0) {
				if (objects.Length > 1) {
					Debug.LogWarning("There is more than one instance of Singleton of type \"" + type +
					                 "\". Keeping the first one. Destroying the others.");
					for (var i = 1; i < objects.Length; i++) Destroy(objects[i].gameObject);
				}
				_instance = objects[0];
				_instance.gameObject.SetActive(true);
				Instantiated = true;
				Destroyed = false;
				return;
			}

			string prefabName;
			GameObject gameObject;
			var attribute = System.Attribute.GetCustomAttribute(type, typeof (PrefabAttribute)) 
				as PrefabAttribute;

			if (attribute == null || string.IsNullOrEmpty(attribute.name)) {
				prefabName = type.ToString();
				gameObject = new GameObject();
			} else {
				prefabName = attribute.name;
				gameObject = Instantiate(Resources.Load<GameObject>(attribute.path + "/" + prefabName));
				if (gameObject == null)
					throw new Exception("Could not find Prefab \"" + prefabName + "\" on Resources for Singleton of type \"" + type +
					                    "\".");
			}

			gameObject.name = prefabName;
			if (_instance == null)
				_instance = gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
			Instantiated = true;
			Destroyed = false;
		}

		public bool persistent;

		public static bool Instantiated { get; private set; }

		public static bool Destroyed { get; private set; }

		protected virtual void Awake() {
			if (_instance == null) {
				if (persistent) {
					CreateInstance();
					DontDestroyOnLoad(gameObject);
				}
				return;
			}
			
			if (persistent) DontDestroyOnLoad(gameObject);
			if (GetInstanceID() != _instance.GetInstanceID()) Destroy(gameObject);
		}

		private void OnDestroy() {
			Destroyed = true;
			Instantiated = false;
		}

		public void Touch() { }
	}
}
