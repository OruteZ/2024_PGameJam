
using System.Collections;
using UnityEngine;
using Utility.Attribute;
using Utility.Generic;

[Prefab("GameManager", "Singleton")]
public class GameManager : Singleton<GameManager>
{
    public int player1Life;
    public int player2Life;

    public GameObject player1;
    public GameObject player2;
    
    public Vector3 player1SpawnPoint;
    public Vector3 player2SpawnPoint;
    
    public Player player1Reference = null;
    public Player player2Reference = null;
    
    public float player1UltimateGauge;
    public float player2UltimateGauge;

    public void GameStart()
    {
        player1Life = 3;
        player2Life = 3;
        
        //spawn immediately
        SpawnPlayerImmediate(1);
        SpawnPlayerImmediate(2);
    }
    
    public void PlayerDie(int playerNum)
    {
        if (playerNum == 1)
        {
            player1Life--;
        }
        else
        {
            player2Life--;
        }
        
        if (IsGameOver() == false)
        {
            StartCoroutine(PlayerSpawn(playerNum, 3f));
        }
    }
    
    public IEnumerator PlayerSpawn(int playerNum, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        var spawnedObj = Instantiate(playerNum == 1 ? player1 : player2);

        // Set position
        spawnedObj.transform.position = playerNum == 1 ? player1SpawnPoint : player2SpawnPoint;
        
        
        spawnedObj.GetComponent<Player>().playerNumber = playerNum;
        
        //set reference and ultimate gauge
        if (playerNum == 1)
        {
            player1Reference = spawnedObj.GetComponent<Player>();
            player1Reference.UltimateGauge = player1UltimateGauge;
        }
        else
        {
            player2Reference = spawnedObj.GetComponent<Player>();
            player2Reference.UltimateGauge = player2UltimateGauge;
        }
    }
    
    public void SpawnPlayerImmediate(int playerNum)
    {
        var spawnedObj = Instantiate(playerNum == 1 ? player1 : player2);

        // Set position
        spawnedObj.transform.position = playerNum == 1 ? player1SpawnPoint : player2SpawnPoint;
        
        
        spawnedObj.GetComponent<Player>().playerNumber = playerNum;
        
        //set con input true
        spawnedObj.GetComponent<Player>().inputController.canInput = true;
        
        //set reference and ultimate gauge
        if (playerNum == 1)
        {
            player1Reference = spawnedObj.GetComponent<Player>();
            player1Reference.UltimateGauge = player1UltimateGauge;
        }
        else
        {
            player2Reference = spawnedObj.GetComponent<Player>();
            player2Reference.UltimateGauge = player2UltimateGauge;
        }
    }
    
    public bool IsGameOver()
    {
        return player1Life <= 0 || player2Life <= 0;
    }

    public void Dead(int deadPlayerNumber)
    {
        PlayerDie(deadPlayerNumber);
        
        //save ultimate gauge
        if (deadPlayerNumber == 1)
        {
            player1UltimateGauge = player1Reference.UltimateGauge;
        }
        else
        {
            player2UltimateGauge = player2Reference.UltimateGauge;
        }
        
        if (IsGameOver())
        {
            Debug.Log("Game Over");
        }
    }

    public Player GetPlayer(int playerNumber)
    {
        //return player reference
        return playerNumber == 1 ? player1Reference : player2Reference;
    }
}
