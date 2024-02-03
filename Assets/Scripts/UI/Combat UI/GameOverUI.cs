using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility.Manager;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private List<Sprite> p1IdleSprites;
    [SerializeField] private List<Sprite> p2IdleSprites;
    [SerializeField] private List<Sprite> p1WinSpriteAfterImages;
    [SerializeField] private List<Sprite> p2WinSpriteAfterImages;
    
    [SerializeField] private Sprite p1WinSprite;
    [SerializeField] private Sprite p2WinSprite;

    
    [SerializeField] private Sprite p1Illustration;
    [SerializeField] private Sprite p2Illustration;
    
    [SerializeField] private Image winnerImage;
    [SerializeField] private Image winnerIllustrationImage;
    [SerializeField] private UISpriteAnimator winnerIdleAnimator;
    [SerializeField] private UISpriteAnimator afterImageAnimator;
    
    public void SetWinner(int playerNum)
    {
        winnerImage.sprite = playerNum == 1 ? p1WinSprite : p2WinSprite;
        winnerIllustrationImage.sprite = playerNum == 1 ? p1Illustration : p2Illustration;
        winnerIdleAnimator.sprites = playerNum == 1 ? p1IdleSprites : p2IdleSprites;
        afterImageAnimator.sprites = playerNum == 1 ? p1WinSpriteAfterImages : p2WinSpriteAfterImages;
        
        //set winner illustration color. 1 player : 74BFFF, 2 player : FF8587
        winnerIllustrationImage.color = playerNum == 1 ? new Color(0.45f, 0.75f, 1f) : new Color(1f, 0.52f, 0.53f);
        
        //if player 2 win, flip the illustration
        winnerIllustrationImage.transform.localScale = playerNum == 1 ? Vector3.one : new Vector3(-1, 1, 1);
    }
    
    public void RestartGame()
    {
        SceneLoadManager.Instance.LoadScene(SceneManager.GetActiveScene().name, true);
        gameObject.SetActive(false);
    }
    
    public void QuitGame()
    {
        SceneLoadManager.Instance.LoadScene("MainMenu", false);
        gameObject.SetActive(false);
    }
    
}
