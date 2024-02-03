using UnityEngine;
using Utility.Manager;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private float moveDistance;

    [SerializeField] List<GameObject> startObj = new List<GameObject>();
    [SerializeField] float startDelay = 3f;

    [SerializeField, Space(5f)] GameObject NonUIObj;

    private void Start()
    {
        foreach (GameObject obj in startObj) obj.SetActive(false);
        Invoke(nameof(PlayBGM), 1f);
        Invoke(nameof(StartObj), startDelay);
    }

    void StartObj()
    {
        foreach (GameObject obj in startObj) obj.SetActive(true);
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
        transform.position += Vector3.left * (moveDistance * direction * Screen.width / 1920);
        NonUIObj.transform.position += Vector3.left * (moveDistance * direction * Screen.width / 1920);
    }
}
