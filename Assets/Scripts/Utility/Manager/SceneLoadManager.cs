using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utility.Attribute;
using Utility.Generic;
using Utility.UI;

namespace Utility.Manager
{
    [Prefab("SceneLoadManager", "Singleton")]
    public class SceneLoadManager : Singleton<SceneLoadManager>
    {
        public UnityEvent onLoadStart = new ();
        public UnityEvent onLoadEnd = new ();
        
        // ReSharper disable once InconsistentNaming
        public float progress { get; private set; }
        
        private void Start()
        {
            progress = -1;
        }

        public LoadingScreen loadingScreen; // Loading 화면을 참조하는 GameObject

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
                loadingScreen.gameObject.SetActive(true); // Loading 화면 켜기
            }
            else
            {
                Debug.Log("Loading screen is null.");
            }

            yield return loadingScreen.FadeIn();

            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone)
            {
                progress = asyncOperation.progress;
                yield return null;
            }

            float delaySeconds = 4;
            yield return new WaitForSeconds(delaySeconds);
            
            yield return loadingScreen.FadeOut();

            if (loadingScreen != null)
            {
                loadingScreen.gameObject.SetActive(false); // Loading 화면 끄기
            }
            else
            {
                Debug.Log("Loading screen is null.");
            }

            progress = -1;
            onLoadEnd.Invoke();
        }
    }
}
