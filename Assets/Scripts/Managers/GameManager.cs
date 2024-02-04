using System.Collections;
using UnityEngine;
using Utility.Attribute;
using Utility.Generic;
using Utility.Manager;

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

    public float player1UltimateGauge = 0;
    public float player2UltimateGauge = 0;

    public UltimateUIEffect ultimateUIEffect1;
    public UltimateUIEffect ultimateUIEffect2;

    public GameOverUI gameOverUI;
    
    public void SetUltimateEffect(int playerNum, UltimateUIEffect effect)
    {
        if (playerNum == 1) ultimateUIEffect1 = effect;
        else ultimateUIEffect2 = effect;
    }
    
    public void UltimateEffectStart(int playerNum)
    {
        if (playerNum == 1) ultimateUIEffect1?.UltimateEffectStart();
        else ultimateUIEffect2?.UltimateEffectStart();
    }

    public void GameStart()
    {
        //set life 3
        player1Life = 3;
        player2Life = 3;
        
        //set ultimate gauge 0
        player1UltimateGauge = 0;
        player2UltimateGauge = 0;
        
        
        SpawnPlayerImmediate(1);
        SpawnPlayerImmediate(2);
    }

    public void PlayerDie(int playerNum)
    {
        if (playerNum == 1) player1Life--;
        else player2Life--;

        if (!IsGameOver())
        {
            StartCoroutine(PlayerSpawn(playerNum, 3f));
        }
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
        
        player.Invincible(2f);
    }

    public bool IsGameOver() => player1Life <= 0 || player2Life <= 0;

    public void Dead(int deadPlayerNumber)
    {
        PlayerDie(deadPlayerNumber);
        if (deadPlayerNumber == 1) player1UltimateGauge = player1Reference.ultimateGauge + 0.2f;
        else player2UltimateGauge = player2Reference.ultimateGauge + 0.2f;

        SoundManager.Instance.PlaySFX("explosion");
        CameraShaker.Instance.ShakeCamera(1f);
        if (IsGameOver())
        {
            SoundManager.Instance.StopBGM();
            StartCoroutine(FinishGame());
            SoundManager.Instance.PlaySFX("game-over");
        }
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
