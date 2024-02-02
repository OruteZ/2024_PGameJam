using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour//카메라 흔들어주는 클래스
{
    static CameraShaker instance;//자유롭게 접근하기 위한 싱글톤
    public static CameraShaker Instance { get { return instance; } }

    [SerializeField] AnimationCurve moveCurve;

    [Space(10f)]
    [SerializeField] float defaultShakePower = 0.5f;
    [SerializeField] float defaultShakeDuration = 0.5f;

    [Space(10f)]
    [SerializeField, Range(0f, 2f)] float powerMultiplier = 1f;

    float shakePower = 01f;

    Vector2 shakeDir;
    float time = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(1)) ShakeCamera();//테스트용 스크립트
#endif

        if (time > 1f) return;
        else
        {
            Vector2 pos = Vector2.zero;
            pos += shakeDir * shakePower * moveCurve.Evaluate(time / defaultShakeDuration);

            transform.localPosition = pos;

            time += Time.deltaTime;

        }
    }

    public void ShakeCamera()
    {
        ShakeCamera(defaultShakePower);
    }

    public void ShakeCamera(float shakePower)
    {
        shakeDir.x = Random.Range(-1f, 1f);
        shakeDir.y = Random.Range(-1f, 1f);

        ShakeCamera(shakePower, shakeDir);
    }

    public void ShakeCamera(float shakePower, Vector2 shakeDir)
    {
        this.shakePower = shakePower * powerMultiplier;

        this.shakeDir = shakeDir;
        this.shakeDir.x += Random.Range(-0.5f, 0.5f);
        this.shakeDir.y += Random.Range(-0.5f, 0.5f);
        this.shakeDir.Normalize();

        //this.shakeDir.Normalize();

        time = 0f;
    }
}
