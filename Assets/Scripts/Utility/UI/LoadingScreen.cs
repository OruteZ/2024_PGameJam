using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public float fadeDuration = 2f;
        
        public IEnumerator FadeIn()
        {
            float timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
                yield return null;
            }
        }
        
        public IEnumerator FadeOut()
        {
            float timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
                yield return null;
            }
        }

    }
}