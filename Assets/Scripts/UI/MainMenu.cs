using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Manager;

public class MainMenu : MonoBehaviour
{
    [SerializeField] 
    private AnimationCurve moveCurve;

    [SerializeField] private float moveDistance;
    [SerializeField] private float moveDuration;

    private void PlayBGM()
    {
        SoundManager.Instance.PlayBGM("MainMenuBGM");
    }

    private void Start()
    {
        Invoke(nameof(PlayBGM), 1f);
    }
    
    public void StartGame()
    {
        SceneLoadManager.Instance.LoadScene("GameScene", true);
        SoundManager.Instance.PlaySFX("menu-select");
        SoundManager.Instance.StopBGM();
    }
    
    public void Option()
    {
        //StartCoroutine(MoveOption(1));
        SetPosition(1);
        SoundManager.Instance.PlaySFX("menu-select");
    }
    
    public void Back()
    {
        //StartCoroutine(MoveOption(-1));
        SetPosition(-1);
        SoundManager.Instance.PlaySFX("menu-select");
    }

    public void KeySetting()
    {
        //StartCoroutine(MoveOption(1));
        SetPosition(1);
        SoundManager.Instance.PlaySFX("menu-select");
    }
    
    public void Exit()
    {
        Application.Quit();
    }
    
    void SetPosition(int dir)
    {
        //var startPos = transform.position;
        var endPos = transform.position + Vector3.left * (moveDistance * dir);

        transform.position = endPos;
    }

    private IEnumerator MoveOption(int direction)
    {
        var startPos = transform.position;
        var endPos = transform.position + Vector3.left * (moveDistance * direction);
        var time = 0f;
        while (time < moveDuration)
        {
            time += Time.deltaTime;
            var t = moveCurve.Evaluate(time / moveDuration);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
    }
}
