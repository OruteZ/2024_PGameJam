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

    float _time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographicSize = firstOffset.camSizeOffset;

        this.transform.position = firstOffset.camPosOffset;

        _time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        Camera.main.orthographicSize = Mathf.Lerp(firstOffset.camSizeOffset,lastOffset.camSizeOffset,_time/delayTime);

        this.transform.position = Vector3.Lerp(firstOffset.camPosOffset, lastOffset.camPosOffset, _time / delayTime);

        _time += Time.deltaTime;
        if (_time >= delayTime) _time = delayTime;
        
    }
}
