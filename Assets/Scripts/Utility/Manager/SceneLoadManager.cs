using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utility.Attribute;
using Utility.Generic;

namespace Utility.Manager
{
    [Prefab("SceneLoadManager", "Singleton")]
    public class SceneLoadManager : Singleton<SceneLoadManager>
    {
        public UnityEvent onLoadStart = new ();
        public UnityEvent onLoadEnd = new ();
        
        // ReSharper disable once InconsistentNaming
        public float progress { get; private set; }

        public GameObject loadingScreen; // Loading 화면을 참조하는 GameObject

        public void LoadScene(string sceneName, bool isAsync = false)
        {
            if (isAsync)
            {
                StartCoroutine(LoadSceneAsync(sceneName));
            }
            else
            {
                onLoadStart.Invoke();
                SceneManager.LoadScene(sceneName);
                onLoadEnd.Invoke();
            }
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            onLoadStart.Invoke();
            if (loadingScreen != null)
            {
                loadingScreen.SetActive(true); // Loading 화면 켜기
            }
            else
            {
                Debug.Log("Loading screen is null.");
            }

            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone)
            {
                progress = asyncOperation.progress;
                yield return null;
            }

            if (loadingScreen != null)
            {
                loadingScreen.SetActive(false); // Loading 화면 끄기
            }
            else
            {
                Debug.Log("Loading screen is null.");
            }
            onLoadEnd.Invoke();
        }
    }
}