using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utility.Manager;
using Utility.ScriptableObject;

public class GameStartTrigger : MonoBehaviour
{
    public TMP_Text countDownText;

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
        
        for(int i = 5; i >= 1 ; i--)
        {
            Call(i);
            yield return new WaitForSeconds(1);
        }
        
        countDownText.text = "START!";
        //todo : play sound
        
        playerInput[0].canInput = true;
        playerInput[1].canInput = true;
        
        yield return new WaitForSeconds(1);
        
        Destroy(gameObject);
    }

    public void Call(int second)
    {
        countDownText.text = second.ToString();
        //todo : play sound
    }
}
