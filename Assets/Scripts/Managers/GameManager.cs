using System.Collections;
using UnityEngine;
using Utility.Generic;

public class GameManager : Singleton<GameManager>
{
    public int player1Life = 3;
    public int player2Life = 3;

    public GameObject player1;
    public GameObject player2;

    public Vector3 player1SpawnPoint;
    public Vector3 player2SpawnPoint;

    public Player player1Reference = null;
    public Player player2Reference = null;

    public float player1UltimateGauge = 0;
    public float player2UltimateGauge = 0;

    public GameOverUI gameOverUI;

    public void GameStart()
    {
        SpawnPlayerImmediate(1);
        SpawnPlayerImmediate(2);
    }

    public void PlayerDie(int playerNum)
    {
        if (playerNum == 1) player1Life--;
        else player2Life--;

        if (!IsGameOver()) StartCoroutine(PlayerSpawn(playerNum, 3f));
    }

    public IEnumerator PlayerSpawn(int playerNum, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SpawnPlayer(playerNum);
    }

    public void SpawnPlayerImmediate(int playerNum)
    {
        SpawnPlayer(playerNum);
        GetPlayer(playerNum).inputController.canInput = true;
    }

    private void SpawnPlayer(int playerNum)
    {
        var spawnedObj = Instantiate(playerNum == 1 ? player1 : player2);
        spawnedObj.transform.position = playerNum == 1 ? player1SpawnPoint : player2SpawnPoint;
        var player = spawnedObj.GetComponent<Player>();
        player.playerNumber = playerNum;

        if (playerNum == 1)
        {
            player1Reference = player;
            player1Reference.ultimateGauge = player1UltimateGauge;
        }
        else
        {
            player2Reference = player;
            player2Reference.ultimateGauge = player2UltimateGauge;
        }
    }

    public bool IsGameOver() => player1Life <= 0 || player2Life <= 0;

    public void Dead(int deadPlayerNumber)
    {
        PlayerDie(deadPlayerNumber);
        if (deadPlayerNumber == 1) player1UltimateGauge = player1Reference.ultimateGauge;
        else player2UltimateGauge = player2Reference.ultimateGauge;

        if (IsGameOver()) StartCoroutine(FinishGame());
    }

    private IEnumerator FinishGame()
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1f;
        gameOverUI.gameObject.SetActive(true);
        gameOverUI.SetWinner(player1Life > 0 ? 1 : 2);
    }

    public Player GetPlayer(int playerNumber) => playerNumber == 1 ? player1Reference : player2Reference;
}
