using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMover : MonoBehaviour
{
    [SerializeField] Vector3 firstPos;

    [SerializeField, Space(5f)] Vector3 lastPos;

    [SerializeField, Space(5f)] float delayTime = 300f;

    float _time;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = firstPos;

        _time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(firstPos, lastPos, _time / delayTime);

        _time += Time.deltaTime;

        if (_time > delayTime) _time = delayTime;
    }
}
