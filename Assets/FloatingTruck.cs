using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTruck : MonoBehaviour
{
    //has no gravity, floating around
    
    public float rotationSpeed;
    public float floatingSpeed;

    public Vector3 target;

    private void Start()
    {
        // every 5 second, set random target
        InvokeRepeating(nameof(SetRandomTarget), 0, 5);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime * 180);
        
        //move to target pos with speed
        transform.position = Vector3.MoveTowards(transform.position, target, floatingSpeed * Time.deltaTime);
        
        //if reach target, set new target
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            SetRandomTarget();
        }
    }

    private void SetRandomTarget()
    {
        target = new Vector3(Random.Range(-8, 8), Random.Range(-3, 3), 0);
    }
}
