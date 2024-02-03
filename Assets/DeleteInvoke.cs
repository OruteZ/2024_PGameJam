using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteInvoke : MonoBehaviour
{
    private void Invoke()
    {
        Invoke(nameof(Destroy), 1);
    }
    void Start()
    {
        Invoke();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
