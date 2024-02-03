using UnityEngine;
using Utility.Manager;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private float moveDistance;

    private void Start()
    {
        Invoke(nameof(PlayBGM), 1f);
    }

    private void PlayBGM()
    {
        SoundManager.Instance.PlayBGM("MainMenuBGM");
    }

    public void StartGame()
    {
        SceneLoadManager.Instance.LoadScene("GameScene", true);
        PlaySoundAndMove(-1);
        
        SoundManager.Instance.StopBGM();
    }

    public void Option()
    {
        PlaySoundAndMove(1);
    }

    public void Back()
    {
        PlaySoundAndMove(-1);
    }

    public void KeySetting()
    {
        PlaySoundAndMove(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void PlaySoundAndMove(int direction)
    {
        SoundManager.Instance.PlaySFX("menu-select");
        transform.position += Vector3.left * (moveDistance * direction);
    }
}
