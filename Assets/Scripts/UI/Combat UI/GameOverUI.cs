using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.Manager;

public class GameOverUI : MonoBehaviour
{
    public void RestartGame()
    {
        SceneLoadManager.Instance.LoadScene(SceneManager.GetActiveScene().name, true);
    }
    
    public void QuitGame()
    {
        SceneLoadManager.Instance.LoadScene("MainMenu", false);
    }
    
}
