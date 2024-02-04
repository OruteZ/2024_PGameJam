using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility.Manager;
using Utility.ScriptableObject;

public class GameStartTrigger : MonoBehaviour
{
    public List<Sprite> countDownSprites;
    public Image countDownImage;
    public Transform startPosition;
    public Transform endPosition;

    public InputController[] playerInput;

    public bool startTrigger = false; 
    
    private void Start()
    {
        playerInput = new InputController[2];
    }
    
    public void Update()
    {
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (SceneLoadManager.Instance.progress == -1 && !startTrigger)
        {
            startTrigger = true;
            StartGame();
        }
    }
    
    public void StartGame()
    {
        StartCoroutine(CountDown());
    }
    
    private IEnumerator CountDown()
    {
        GameManager.Instance.GameStart();
        //get two player's input from gameManager 
        yield return new WaitWhile(() => (GameManager.Instance.player1Reference == null));
        
        playerInput[0] = GameManager.Instance.player1Reference.inputController;
        playerInput[1] = GameManager.Instance.player2Reference.inputController;
        
        //disable
        playerInput[0].canInput = false;
        playerInput[1].canInput = false;

        yield return new WaitForSeconds(1f);
        
        SoundManager.Instance.PlaySFX("countdown");

        for(int i = 1; i < 5; i++)
        {
            Call(i);
            if(i != 4) yield return new WaitForSeconds(1);
        }

        
        playerInput[0].canInput = true;
        playerInput[1].canInput = true;
        
        yield return new WaitForSeconds(0.3f);
        
        SoundManager.Instance.PlayBGM("CombatBGM");

        // Destroy(gameObject);
    }

    private IEnumerator ImageLerp(Vector3 startPositionPosition, Vector3 endPositionPosition)
    {
        //1 second lerp
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            countDownImage.transform.position = Vector3.Lerp(startPositionPosition, endPositionPosition, time);
            yield return null;
        }
    }

    public void Call(int second)
    {
        countDownImage.sprite = countDownSprites[second - 1];
        
        if(second == 4) 
            StartCoroutine(ImageLerp(endPosition.position, startPosition.position));
        else if(second == 1) 
            StartCoroutine(ImageLerp(startPosition.position, endPosition.position));
            
    }
}
