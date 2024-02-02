using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    public float speed = 1f;

    private void Update()
    {
        //spin 2D object
        transform.Rotate(0, 0, -speed * Time.deltaTime * 360f);
    }
}
