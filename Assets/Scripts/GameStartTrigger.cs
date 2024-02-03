using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartTrigger : MonoBehaviour
{
    public void Awake()
    {
        GameManager.Instance.GameStart();
    }
}
