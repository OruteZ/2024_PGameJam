using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utility.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public float lerpDuration = 2f;

        public Image programmingImage;
        public Image artImage;
        public Image loadingImage;
        public Image vsImage;

        public Vector2 inCline = new Vector2(20, 23);

        public AnimationCurve lerpCurve;
        
        public float outOfScreenDistance = 1500f;
        
        public IEnumerator FadeIn()
        {
            //art image comes from left up (depends on inCline)
            //programming image comes from right down
            float time = 0;
            while (time < lerpDuration)
            {
                time += Time.deltaTime;
                float lerp = lerpCurve.Evaluate(time / lerpDuration);
                programmingImage.rectTransform.anchoredPosition = 
                    Vector2.Lerp(inCline.normalized * outOfScreenDistance, Vector2.zero, lerp);
                artImage.rectTransform.anchoredPosition = 
                    Vector2.Lerp(-inCline.normalized * outOfScreenDistance, Vector2.zero, lerp);
                yield return null;
            }
            
            loadingImage.gameObject.SetActive(true);
            //vs image too 
            vsImage.gameObject.SetActive(true);
        }
        
        public IEnumerator FadeOut()
        {
            loadingImage.gameObject.SetActive(false);
            vsImage.gameObject.SetActive(false);
            //art image goes to left down (depends on inCline)
            //programming image goes to right up
            float time = 0;
            while (time < lerpDuration)
            {
                time += Time.deltaTime;
                float lerp = lerpCurve.Evaluate(time / lerpDuration);
                programmingImage.rectTransform.anchoredPosition = 
                    Vector2.Lerp(Vector2.zero, inCline.normalized * outOfScreenDistance, lerp);
                artImage.rectTransform.anchoredPosition = 
                    Vector2.Lerp(Vector2.zero, -inCline.normalized * outOfScreenDistance, lerp);
                yield return null;
            }
        }

    }
}
