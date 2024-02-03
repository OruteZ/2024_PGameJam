using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartTrigger : MonoBehaviour
{
    public void Start()
    {
        GameManager.Instance.GameStart();
    }
}
