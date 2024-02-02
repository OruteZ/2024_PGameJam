using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Manager;

public class MainMenu : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneLoadManager.Instance.LoadScene("GameScene", true);
    }
    
    public void Option()
    {
    }
    
    public void Exit()
    {
        Application.Quit();
    }
}
