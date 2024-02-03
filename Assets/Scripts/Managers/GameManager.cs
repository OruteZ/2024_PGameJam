
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
    
    public Player player1Reference;
    public Player player2Reference;

    public void GameStart()
    {
        player1Life = 3;
        player2Life = 3;
        
        StartCoroutine(PlayerSpawn(1, 0f));
        StartCoroutine(PlayerSpawn(2, 0f));
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
        
        //set reference
        if (playerNum == 1)
        {
            player1Reference = spawnedObj.GetComponent<Player>();
        }
        else
        {
            player2Reference = spawnedObj.GetComponent<Player>();
        }
    }
    
    public bool IsGameOver()
    {
        return player1Life <= 0 || player2Life <= 0;
    }

    public void Dead(int deadPlayerNumber)
    {
        PlayerDie(deadPlayerNumber);
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
