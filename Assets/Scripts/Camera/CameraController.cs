using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [System.Serializable]
    class CamOffset
    {
        public float camSizeOffset;

        public Vector3 camPosOffset;
    }

    [SerializeField]
    CamOffset firstOffset;

    [SerializeField, Space(5f)]
    CamOffset lastOffset;

    [SerializeField, Space(5f)]
    float delayTime;
    
    public AnimationCurve lerpCurve;

    [Header("Movement")]
    public float movementDelay = 2f;
    public float movementStrength = 1f;

    float _time = 0f;
    float _time2 = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographicSize = firstOffset.camSizeOffset;

        this.transform.position = firstOffset.camPosOffset;

        _time = 0f;
        _time2 = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float lerp = lerpCurve.Evaluate(_time / delayTime);
        
        Camera.main.orthographicSize = Mathf.Lerp(firstOffset.camSizeOffset,lastOffset.camSizeOffset,lerp);

        Vector3 pos = Vector3.Lerp(firstOffset.camPosOffset, lastOffset.camPosOffset, lerp);
        pos += Vector3.up * Mathf.Sin(_time2 / movementDelay) * movementStrength;

        this.transform.position = pos;

        _time += Time.deltaTime;
        _time2 += Time.deltaTime;
        if (_time >= delayTime) _time = delayTime;
        
    }
}
