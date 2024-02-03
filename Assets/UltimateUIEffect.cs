using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateUIEffect : MonoBehaviour
{
    public int playerNum;
    
    public GameObject ultimateImage;
    public AnimationCurve curve;

    public float duration;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 endPos;
    
    private void Awake()
    {
        gameObject.SetActive(false);
        GameManager.Instance.SetUltimateEffect(playerNum, this);
    }

    public void UltimateEffectStart()
    {
        gameObject.SetActive(true);

        StartCoroutine(ImageMoveCoroutine());
    }

    private IEnumerator ImageMoveCoroutine()
    {
        // lerp
        float time = 0;
        while (time < duration)
        {
            ultimateImage.transform.localPosition = Vector2.Lerp(startPos, endPos, curve.Evaluate(time / duration));
            time += Time.deltaTime;
            yield return null;
        }
        
        gameObject.SetActive(false);
    }

    public void OnDestroy()
    {
        GameManager.Instance.SetUltimateEffect(playerNum, null);
    }
}
