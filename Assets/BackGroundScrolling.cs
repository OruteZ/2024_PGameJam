using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundScrolling : MonoBehaviour
{
    public Transform start;
    public Transform end;
    
    public float speed = 1f;
    
    public List<GameObject> images;
    
    private void Update()
    {
        foreach (var image in images)
        {
            image.transform.position += Vector3.right * (speed * Time.deltaTime);
            if (image.transform.position.x > end.position.x)
            {
                image.transform.position = start.position;
            }
        }
    }
}
